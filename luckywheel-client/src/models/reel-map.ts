import type {Color} from "@/types/color.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";

export class ReelMap extends WorkshopMap {
  constructor(public mapName: string,
              public imageUrl: string,
              public description: string,
              public workshopId: string,
              public weight: number,
              public color: Color) {
    super(mapName, imageUrl, description, workshopId);
  }
}
