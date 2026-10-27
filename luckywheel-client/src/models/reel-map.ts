import type {Color} from "@/types/color.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";
import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";
import {SortOptions} from "@/stores/sorting-options.store.ts";

export class ReelMap extends WorkshopMap {
  constructor(public mapName: string,
              public imageUrl: string,
              public description: string,
              public workshopId: string,
              public weight: number,
              public color: Color,
              public hasPlayed: boolean,
              public rating: number,
              public terroristSpawns?: number,
              public counterTerroristSpawns?: number) {
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
      this.rating,
      this.terroristSpawns,
      this.counterTerroristSpawns
    )
  }

  public static getComparator(sortOptions: SortOptions): (a: ReelMap, b: ReelMap) => number {
    switch (sortOptions) {
      case SortOptions.NAME:
        return (a, b) => a.mapName.localeCompare(b.mapName);
      case SortOptions.RARITY:
        return (a, b) => {
          if (a.hasPlayed && !b.hasPlayed) return 1;
          if (!a.hasPlayed && b.hasPlayed) return -1;
          return a.weight - b.weight
        }
      case SortOptions.RATING:
        return (a, b) => a.rating - b.rating;
    }
  }
}
