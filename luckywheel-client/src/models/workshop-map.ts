import type {Color} from "@/types/color.ts";
import {ReelMap} from "@/models/reel-map.ts";
import type {WorkshopMapDto} from "@/api/dto/WorkshopMapDto.ts";

export class WorkshopMap {

  constructor(public workshopName: string,
              public workshopId: string,
              public weight: number,
              public imageSource: string) {

  }

  toReelMap(color: Color): ReelMap {
    return new ReelMap(this.workshopName, this.workshopId, this.weight, color);
  }

  public static fromDto(dto: WorkshopMapDto) {
    return new WorkshopMap(dto.workshopName, dto.workshopId, dto.weight, dto.imageSource);
  }
}
