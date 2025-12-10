import type {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";

export class GetUserPlaylistsResultDto {
  constructor(public workshopPlaylists: WorkshopPlaylistIndex[]){}
}
