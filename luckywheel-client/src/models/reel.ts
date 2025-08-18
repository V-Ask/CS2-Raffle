import type {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";

export class Reel {

  private workshopMaps: WorkshopMap[] = [];

  constructor(playlist: WorkshopPlaylist, reelLength: number = 100) {
    this.buildReel(playlist, reelLength);
  }

  private buildReel(playlist: WorkshopPlaylist, reelLength: number) {
    const totalWeight = this.calculateTotalWeight(playlist);
    for (let i = 0; i < reelLength; i++) {

    }
  }

  private getWeight

  private calculateTotalWeight(playlist: WorkshopPlaylist) {
    return playlist.maps.reduce((previousValue, currentValue) => {
      return previousValue + currentValue.rarity;
    }, 0);
  }
}
