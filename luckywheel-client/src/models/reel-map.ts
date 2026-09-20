import type {Color} from "@/types/color.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";
import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";

export class ReelMap extends WorkshopMap {
  constructor(public mapName: string,
              public imageUrl: string,
              public description: string,
              public workshopId: string,
              public weight: number,
              public color: Color,
              public hasPlayed: boolean,
              public rating: number) {
    super(mapName, imageUrl, description, workshopId);
  }

  public toView() {
    return new WorkshopPlaylistMapView(
      this.mapName,
      this.imageUrl,
      this.description,
      this.mapId,
      this.weight,
      this.hasPlayed,
      this.rating
    )
  }
}
