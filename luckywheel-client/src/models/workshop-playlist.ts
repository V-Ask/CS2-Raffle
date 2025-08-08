import {type WorkshopMap} from "@/models/workshop-map.ts";

export class WorkshopPlaylist {
  playlistName: string;
  workshopMaps: WorkshopMap[];

  constructor(playlistName: string, workshopMaps: WorkshopMap[]) {
    this.playlistName = playlistName;
    this.workshopMaps = workshopMaps;
  }
}
