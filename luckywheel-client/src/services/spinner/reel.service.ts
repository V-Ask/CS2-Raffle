import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import {COLOR_RARITY_PAIRS} from "@/helpers/constants/colors.ts";
import type {ReelMap} from "@/models/reel-map.ts";
import {WeightedList} from "@/models/weighted-list.ts";

export default {
  colorPlaylist(playlist: WorkshopPlaylistView) {
    const sortedPlaylist = playlist.getSorted({
      comparison: "weight",
      ascending: false,
    });
    const segmentSizeMap = COLOR_RARITY_PAIRS.map(color => {
      return Math.ceil((color.chance / 100) * sortedPlaylist.length);
    })
    let coloredMaps = new Set<ReelMap>();
    let currentIndex = 0;
    let currentSegment = 0;
    let totalSegments = 0;
    while (currentSegment < segmentSizeMap.length && currentIndex < sortedPlaylist.length) {
      totalSegments = segmentSizeMap[currentSegment] + totalSegments;
      const color = COLOR_RARITY_PAIRS[currentSegment];
      while (currentIndex < totalSegments) {
        coloredMaps.add(sortedPlaylist[currentIndex].toReelMap(color.color));
        currentIndex++;
      }
      currentSegment++;
    }
    return coloredMaps;
  },

  buildRandomReel(reelSize: number, coloredMaps: Set<ReelMap>) {
    const weightedMaps = WeightedList.fromSetMap(coloredMaps, map => map.weight)
    const reel: ReelMap[] = [];
    for (let i = 0; i < reelSize; i++) {
      reel.push(weightedMaps.getRandom());
    }
    return reel;
  }
}
