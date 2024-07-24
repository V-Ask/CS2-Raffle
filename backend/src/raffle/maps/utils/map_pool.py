class Map:
    def __init__(self, name: str, id: int, image_url: str, weight: int) -> None:
        self.name = name
        self.id = id
        self.image_url = image_url
        self.weight = weight

    def increase_weight(self):
        self.weight += 1

    def map_dict(self):
        return {'name': self.name, 'id': self.id, 'image_url': self.image_url, 'weight': self.weight}

class MapPool:
    def __init__(self, maps, played_maps) -> None:
        self.maps = maps
        self.played_maps = played_maps

    def add_map(self, map: Map):
        if not map is None:
            self.maps[map.id] = map

    def get_map(self, id: str):
        if id in self.maps:
            return self.maps[id]
        if id in self.played_maps:
            return self.played_maps[id]
        return None

    def remove_map(self, id: str):
        if id in self.maps:
            map = self.maps[id]
            self.played_maps[id] = map
            del self.maps[id]
    
    def unplay_map(self, id: str):
        if id in self.played_maps:
            map = self.played_maps[id]
            self.maps[id] = map
            del self.played_maps[id]
    
    def increase_all_other_weights(self, excluded: str):
        for id in self.maps:
            if id == excluded:
                continue
            self.maps[id].increase_weight()