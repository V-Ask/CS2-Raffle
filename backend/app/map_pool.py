class Map:
    def __init__(self, name: str, id: str, weight: int) -> None:
        self.name = name
        self.id = id
        self.weight = weight

class MapPool:
    def __init__(self) -> None:
        self.maps = set()

    def add_map(self, map: Map):
        self.maps.add(map)

    def get_reel(self, length: int):
        return []