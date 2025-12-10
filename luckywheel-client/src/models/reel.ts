import  {type WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import  {type ReelMap} from "@/models/reel-map.ts";
import {COLOR_RARITY_PAIRS} from "@/helpers/constants/colors.ts";
import {WeightedList} from "@/models/weighted-list.ts";

export class Reel {
  get coloredMaps(): ReelMap[] {
    return this._coloredMaps;
  }

  private readonly _coloredMaps: ReelMap[];

  constructor(playlist: WorkshopPlaylistView) {
    this._coloredMaps = this.colorPlaylist(playlist);
  }

  public buildRandomReel(reelSize: number) {
    const weightedMaps = WeightedList.fromArrayMap(this.coloredMaps, map => map.weight)
    const reel: ReelMap[] = [];
    for (let i = 0; i < reelSize; i++) {
      reel.push(weightedMaps.getRandom());
    }
    return reel;
  }

  private colorPlaylist(playlist: WorkshopPlaylistView) {
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
