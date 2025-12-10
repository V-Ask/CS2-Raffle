import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";

export class WorkshopPlaylistIndex {
  constructor(public collectionName: string,
              public playlistId: string,) {
  }

  public static fromPlaylist(playlist: WorkshopPlaylistView): WorkshopPlaylistIndex {
    return new WorkshopPlaylistIndex(playlist.collectionName, playlist.playlistId);
  }
}
