import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";
import type {WorkshopMapDto} from "@/api/dto/workshop-map-dto.ts";

export class WorkshopMap {

  constructor(public workshopName: string,
              public imageUrl: string,
              public description: string,
              public mapId: string) {

  }
  toPlaylistMap(playlistId: string): WorkshopPlaylistMapView {
    return new WorkshopPlaylistMapView(this.workshopName, this.imageUrl, this.description, this.mapId, playlistId, 1, false);
  }

  public static fromDto(dto: WorkshopMapDto): WorkshopMap {
    console.log("dto1111", dto);
    return new WorkshopMap(dto.name, dto.imageSource, dto.description, dto.workshopId);
  }
}
