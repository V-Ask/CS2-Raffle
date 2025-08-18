import {type WorkshopMap} from "@/models/workshop-map.ts";

export class WorkshopPlaylist {

  constructor(public playlistId: string,
              public collectionName: string,
              public maps: WorkshopMap[]) {
  }
}
