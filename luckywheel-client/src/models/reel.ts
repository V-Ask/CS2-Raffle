import  {type WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import  {type ReelMap} from "@/models/reel-map.ts";
import {COLOR_RARITY_PAIRS} from "@/helpers/constants/colors.ts";
import {WeightedList} from "@/models/weighted-list.ts";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import {REEL_LENGTH} from "@/helpers/constants/reel.ts";

export class Reel {
  get coloredMaps(): ReelMap[] {
    return this._coloredMaps;
  }

  private readonly _coloredMaps: ReelMap[];

  constructor(playlist: WorkshopPlaylist) {
    this._coloredMaps = this.colorPlaylist(playlist);
  }

  public buildReel() {
    const weightedMaps = WeightedList.fromArrayMap(this.coloredMaps, map => map.weight)
    const reel: ReelMap[] = [];
    for (let i = 0; i < REEL_LENGTH; i++) {
      reel.push(weightedMaps.getRandom());
    }
    return reel;
  }

  private colorPlaylist(playlist: WorkshopPlaylist) {
    const sortedPlaylist = playlist.getSorted({
      comparison: "weight",
      ascending: false,
    });
    const segmentSizeMap = COLOR_RARITY_PAIRS.map(color => {
      return Math.ceil((color.chance / 100) * sortedPlaylist.length);
    })
    let coloredMaps: ReelMap[] = [];
    let currentIndex = 0;
    let currentSegment = 0;
    let totalSegments = 0;
    while (currentSegment < segmentSizeMap.length && currentIndex < sortedPlaylist.length) {
      totalSegments = segmentSizeMap[currentSegment] + totalSegments;
      const color = COLOR_RARITY_PAIRS[currentSegment];
      while (currentIndex < totalSegments) {
        coloredMaps.push(sortedPlaylist[currentIndex].toReelMap(color.color));
        currentIndex++;
      }
      currentSegment++;
    }
    return coloredMaps;
  }
}
