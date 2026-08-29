import type {WorkshopPlaylistViewDto} from "@/api/dto/views/workshop-playlist-view-dto.ts";
import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";

export class WorkshopPlaylistView {

  constructor(public playlistId: string,
              public collectionName: string,
              public maps: WorkshopPlaylistMapView[],
              public created: Date,
              public modified: Date,) {
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

  addNewMap(workshopMap: WorkshopMap) {
    this.maps.push(new WorkshopPlaylistMapView(workshopMap.workshopName, workshopMap.imageUrl, workshopMap.description, workshopMap.mapId, this.playlistId, 1, false));
  }

  async incrementAllOtherMaps(excludedMap: WorkshopMap, shouldRemove: boolean) {
    this.maps.forEach((workshopMap) => {
      workshopMap.weight++;
    });
    if (shouldRemove) {
      this.removeMapFromArray(excludedMap);
    }
    return PlaylistService.incrementAllOtherMaps(this, excludedMap, shouldRemove)
  }

  async removeMap(removedMap: WorkshopMap) {
    this.removeMapFromArray(removedMap);
    return PlaylistService.removeMapFromPlaylist(removedMap, this);
  }

  removeMapFromArray(removedMap: WorkshopMap) {
    this.maps = this.maps.filter(map => map.mapId !== removedMap.mapId);
  }

  toIndex() {
    return new WorkshopPlaylistIndex(this.collectionName, this.playlistId, this.maps.length, this.created, this.modified);
  }

  public static fromDto(dto: WorkshopPlaylistViewDto): WorkshopPlaylistView {
    const maps = dto.maps.map(map => WorkshopPlaylistMapView.fromDto(map));
    return new WorkshopPlaylistView(dto.id, dto.collectionName, maps, dto.created, dto.modified);
  }
}

type SortOptions = {
  comparison: "name" | "weight",
  ascending: boolean,
}
