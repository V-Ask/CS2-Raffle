import {WorkshopMap} from "@/models/workshop-map.ts";
import type {WorkshopPlaylistDto} from "@/api/dto/WorkshopPlaylistDto.ts";

export class WorkshopPlaylist {

  constructor(public playlistId: string,
              public collectionName: string,
              public maps: WorkshopMap[]) {
  }

  getSorted(sortOptions: SortOptions) {
    let comparator = this.getComparator(sortOptions);
    const sorted = this.maps.sort(comparator);
    if (sortOptions.ascending) {
      return sorted;
    }
    return sorted.reverse();
  }

  getComparator(sortOptions: SortOptions) {
    let comparator: (a: WorkshopMap, b: WorkshopMap) => number;
    switch (sortOptions.comparison) {
      case "name":
        comparator = (a, b) => a.workshopName.localeCompare(b.workshopName);
        break;
      case "weight":
        comparator = (a, b) => a.weight - b.weight;
        break;
      default:
        comparator = (a, b) => a.weight - b.weight;
        break;
    }
    return comparator;
  }

  addMap(workshopMap: WorkshopMap) {
    this.maps.push(workshopMap);
  }

  public static fromDto(dto: WorkshopPlaylistDto): WorkshopPlaylist {
    const maps = dto.maps.map(map => WorkshopMap.fromDto(map));
    return new WorkshopPlaylist(dto.playlistId, dto.collectionName, maps);
  }
}

type SortOptions = {
  comparison: "name" | "weight",
  ascending: boolean,
}
