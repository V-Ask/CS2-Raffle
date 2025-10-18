import type {WorkshopMapDto} from "@/api/dto/WorkshopMapDto.ts";

export class WorkshopPlaylistDto {
  constructor(public playlistId: string,
              public collectionName: string,
              public maps: WorkshopMapDto[]) {
  }
}
