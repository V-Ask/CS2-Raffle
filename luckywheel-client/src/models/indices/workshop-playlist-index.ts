import type {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import PlaylistApi from "@/api/playlist-api.ts";

export class WorkshopPlaylistIndex {
  constructor(public collectionName: string,
              public playlistId: string,) {
  }
}
