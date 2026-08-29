import type {WorkshopMapDto} from "@/api/dto/workshop-map-dto.ts";

export class WorkshopMap {

  constructor(public workshopName: string,
              public imageUrl: string,
              public description: string,
              public mapId: string) {

  }

  public static fromDto(dto: WorkshopMapDto): WorkshopMap {
    return new WorkshopMap(dto.name, dto.imageUrl, dto.description, dto.mapId);
  }
}
