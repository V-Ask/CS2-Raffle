import type {WorkshopPlaylistViewDto} from "@/api/dto/views/workshop-playlist-view-dto.ts";
import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";
import {WorkshopPlaylistMapViewDto} from "@/api/dto/views/workshop-playlist-map-view-dto.ts";

export class WorkshopPlaylistView {

  constructor(public playlistId: string,
              public collectionName: string,
              public maps: WorkshopPlaylistMapView[]) {
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
    let comparator: (a: WorkshopPlaylistMapView, b: WorkshopPlaylistMapView) => number;
    switch (sortOptions.comparison) {
      case "name":
        comparator = (a, b) => a.name.localeCompare(b.name);
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

  addMap(workshopMap: WorkshopPlaylistMapView) {
    this.maps.push(workshopMap);
  }

  public static fromDto(dto: WorkshopPlaylistViewDto): WorkshopPlaylistView {
    const maps = dto.maps.map(map => WorkshopPlaylistMapView.fromDto(map));
    return new WorkshopPlaylistView(dto.id, dto.collectionName, maps);
  }
}

type SortOptions = {
  comparison: "name" | "weight",
  ascending: boolean,
}
