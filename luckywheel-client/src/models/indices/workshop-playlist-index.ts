import type {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import Playlist from "@/api/playlist.ts";

export class WorkshopPlaylistIndex {
  constructor(public collectionName: string,
              public playlistId: string,) {
  }

  async getPlaylist(): Promise<WorkshopPlaylist> {
    return Playlist.getPlaylist(this.playlistId);
  }
}
