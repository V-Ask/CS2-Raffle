import type {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import PlaylistApi from "@/api/playlist-api.ts";
import type {WorkshopPlaylistDto} from "@/api/dto/WorkshopPlaylistDto.ts";

export class WorkshopPlaylistIndex {
  constructor(public collectionName: string,
              public playlistId: string,) {
  }

  public static fromDto(dto: WorkshopPlaylistDto): WorkshopPlaylistIndex {
    return new WorkshopPlaylistIndex(dto.collectionName, dto.collectionName);
  }
}
