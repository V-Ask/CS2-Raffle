import type {Color} from "@/types/color.ts";

export class ReelMap {
  constructor(public mapName: string,
              public workshopId: string,
              public weight: number,
              public color: Color) {
  }
}
