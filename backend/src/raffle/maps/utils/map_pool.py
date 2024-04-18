import random, json

class Map:
    def __init__(self, name: str, id: str, image_url: str, weight: int) -> None:
        self.name = name
        self.id = id
        self.image_url = image_url
        self.weight = weight

    def increse_weight(self):
        self.weight += 1

class MapPool:
    def __init__(self) -> None:
        self.maps = dict()

    def add_map(self, map: Map):
        if not map is None:
            self.maps[map.id] = map

    def remove_map(self, id: str):
        self.maps(id, None)
    
    def increase_played_weight(self, id: str):
        if id in self.maps:
            self.maps[id].increse_weight()

    def get_reel(self, length: int):
        options = [[map_] * map_.weight for map_ in self.maps.values()]
        flattened = sum(options, [])
        return random.choices(flattened, length)