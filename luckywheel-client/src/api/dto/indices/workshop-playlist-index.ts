import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";

export class WorkshopPlaylistIndex {
  constructor(public collectionName: string,
              public playlistId: string,
              public size: number,
              public created: Date,
              public modified: Date,) {
  }
}
