from .utils import map_pool, workshop_scraper
from raffle.database import database_manager

def workshop_item_to_map(item: workshop_scraper.WorkshopItem) -> map_pool.Map:
    return map_pool.Map(item.name, item.id, item.image_url, 1)

def database_values_to_map_dict(values):
    output = dict()
    for (name, workshop_id, image_url, weight) in values:
        output[workshop_id] = map_pool.Map(name, workshop_id, image_url, weight)
    return output

class MapManager:
    def __init__(self, path) -> None:
        self.database = database_manager.Database(path)
        self.map_pool = map_pool.MapPool(database_values_to_map_dict(self.database.get_maps()))

    def add_map(self, url: str):
        workshop_item = workshop_scraper.get_workshop_item(url)
        if workshop_item is None:
            return
        map = workshop_item_to_map(workshop_item)
        self.map_pool.add_map(map)
        self.database.add_map(map.name, map.id, map.image_url, map.weight)

    def remove_map(self, id: str):
        self.map_pool.remove_map(id)
        self.database.remove_map(id)

    def play_map(self, id: str):
        self.map_pool.increase_all_other_weights(id)
        self.database.increase_all_other_weights(id)

    def get_maps(self):
        print(self.map_pool.maps)
        return list(self.map_pool.maps.values())
    
    def generate_reel(self, length):
        return self.map_pool.get_reel(length)
    