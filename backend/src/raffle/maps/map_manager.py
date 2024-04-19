from .utils import map_pool, workshop_scraper

def workshop_item_to_map(item: workshop_scraper.WorkshopItem) -> map_pool.Map:
    return map_pool.Map(item.name, item.id, item.image_url, 1)

class MapManager:
    def __init__(self) -> None:
        self.map_pool = map_pool.MapPool()

    def add_map(self, url: str):
        workshop_item = workshop_scraper.get_workshop_item(url)
        if workshop_item is None:
            return
        self.map_pool.add_map(workshop_item_to_map(workshop_item))