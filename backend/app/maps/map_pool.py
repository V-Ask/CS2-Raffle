import random
from scraper.workshop_scraper import WorkshopMap, WorkshopScraper

class Map:
    def __init__(self, name: str, id: str, image_url: str, weight: int) -> None:
        self.name = name
        self.id = id
        self.image_url = image_url
        self.weight = weight

class MapPool:
    def __init__(self) -> None:
        self.maps = set()

    def add_map(self, map: Map):
        if not map is None:
            self.maps.add(map)

    def get_reel(self, length: int):
        options = [[map_] * map_.weight for map_ in self.maps]
        flattened = sum(options, [])
        return random.choices(flattened, length)
    
def scrape_to_map(scrape: WorkshopMap, id: str):
    if scrape is None:
        return None
    return Map(scrape.name, id, scrape.image_url, 1)

def get_workshop_scrape(url: str):
    