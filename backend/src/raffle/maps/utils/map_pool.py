import random

class Map:
    def __init__(self, name: str, id: int, image_url: str, weight: int) -> None:
        self.name = name
        self.id = id
        self.image_url = image_url
        self.weight = weight

    def increse_weight(self):
        self.weight += 1

    def map_dict(self):
        {'name': self.name, 'id': self.id, 'image_url': self.image_url, 'weight': self.weight}

class MapPool:
    def __init__(self, maps) -> None:
        self.maps = maps

    def add_map(self, map: Map):
        if not map is None:
            self.maps[map.id] = map

    def remove_map(self, id: str):
        del self.maps[id]
    
    def increase_played_weight(self, id: str):
        if id in self.maps:
            self.maps[id].increse_weight()

    def get_reel(self, length: int):
        if len(self.maps) == 0:
            return []
        options = [[map_] * map_.weight for map_ in self.maps.values()]
        flattened = sum(options, [])
        return random.choices(flattened, k=length)
    
    def increase_all_other_weights(self, excluded: str):
        for id in self.maps:
            if id == excluded:
                continue
            self.maps[id] += 1