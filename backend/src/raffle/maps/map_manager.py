from .utils import map_pool, workshop_scraper
from raffle.database import database_manager

def workshop_item_to_map(item: workshop_scraper.WorkshopItem) -> map_pool.Map:
    return map_pool.Map(item.name, item.id, item.image_url, 1)

def database_to_dict(values):
    output = dict()
    for (name, workshop_id, image_url, weight) in values:
        output[workshop_id] = map_pool.Map(name, workshop_id, image_url, weight)
    return output

class MapManager:
    def __init__(self, path) -> None:
        self.database = database_manager.Database(path)
        non_played, played = self.database.get_nonplayed_maps(), self.database.get_played_maps()
        self.map_pool = map_pool.MapPool(database_to_dict(non_played), database_to_dict(played))

    def add_map(self, url: str):
        workshop_item = workshop_scraper.get_workshop_item(url)
        if workshop_item is None:
            return
        map = workshop_item_to_map(workshop_item)
        self.map_pool.add_map(map)
        self.database.add_map(map.name, map.id, map.image_url, map.weight)

    def remove_map(self, id: str):
        map = self.map_pool.get_map(id)
        self.map_pool.remove_map(id)
        self.database.remove_map(map.name, map.id, map.image_url, map.weight)

    def play_map(self, id: str):
        self.map_pool.increase_all_other_weights(id)
        self.database.increase_all_other_weights(id)

    def get_nonplayed(self):
        return list(self.map_pool.maps.values())
    
    def get_played(self):
        return list(self.map_pool.played_maps.values())
    
    def generate_reel(self, length):
        return self.map_pool.get_reel(length)
    