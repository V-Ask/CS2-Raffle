import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";

export class WorkshopPlaylistMapViewDto {
  constructor(
    public name: string,
    public imageUrl: string,
    public description: string,
    public mapId: string,
    public playlistId: string,
    public weight: number,
    public hasPlayed: boolean,
  ) {
  }

}
