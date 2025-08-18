import type {Color} from "@/types/color.ts";
import {ReelMap} from "@/models/reel-map.ts";

export class WorkshopMap {
  workshopName: string;
  workshopId: string;
  rarity: number;
  imageSource: string;

  constructor(workshopName: string, workshopId: string, rarity: number, imageSource: string) {
    this.workshopName = workshopName;
    this.workshopId = workshopId;
    this.rarity = rarity;
    this.imageSource = imageSource;
  }

  toReelMap(color: Color): ReelMap {
    return new ReelMap(this.workshopName, this.workshopId, color);
  }
}
