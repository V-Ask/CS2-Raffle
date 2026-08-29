import type {WorkshopPlaylistMapViewDto} from "@/api/dto/views/workshop-playlist-map-view-dto.ts";

export class WorkshopPlaylistViewDto {
  constructor(
    public collectionName: string,
    public id: string,
    public maps: WorkshopPlaylistMapViewDto[]
  ) {
  }
}
