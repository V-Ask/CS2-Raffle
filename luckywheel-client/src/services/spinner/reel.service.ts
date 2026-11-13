import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import {COLOR_RARITY_PAIRS} from "@/helpers/constants/colors.ts";
import {ReelMap} from "@/models/reel-map.ts";
import {WeightedList} from "@/models/weighted-list.ts";
import type {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";
import type {Color} from "@/types/color.ts";

function toReelMap(workshopPlaylistMapView: WorkshopPlaylistMapView, color: Color): ReelMap {
  return new ReelMap(
    workshopPlaylistMapView.name,
    workshopPlaylistMapView.imageUrl,
    workshopPlaylistMapView.description,
    workshopPlaylistMapView.mapId,
    workshopPlaylistMapView.weight,
    color,
    workshopPlaylistMapView.hasPlayed,
    workshopPlaylistMapView.rating,
    workshopPlaylistMapView.terroristSpawns,
    workshopPlaylistMapView.counterTerroristSpawns
  );
}

function assignColors(sortedMaps: WorkshopPlaylistMapView[]) {
  const length = sortedMaps.length;
  const segments = COLOR_RARITY_PAIRS.length;
  const coloredMaps = new Set<ReelMap>();

  if (length < segments) {
    sortedMaps.forEach((map, i) => coloredMaps.add(toReelMap(map, COLOR_RARITY_PAIRS[i].color)));
    return coloredMaps;
  }

  const pivotIndices = COLOR_RARITY_PAIRS.map((_, i) => Math.round(i * (length - 1) / (segments - 1)));
  let lower = 0;
  for (let i = 0; i < length; i++) {
    while (lower < segments - 1 && pivotIndices[lower + 1] <= i) {
      lower++;
    }
    const map = sortedMaps[i];
    if (lower == segments - 1 || pivotIndices[lower] === i) {
      coloredMaps.add(toReelMap(map, COLOR_RARITY_PAIRS[lower].color));
      continue;
    }
    const upper = lower + 1;
    const lowerDiff = Math.abs(map.weight - sortedMaps[pivotIndices[lower]].weight);
    const upperDiff = Math.abs(map.weight - sortedMaps[pivotIndices[upper]].weight);
    if (lowerDiff !== upperDiff) {
      coloredMaps.add(toReelMap(map, lowerDiff < upperDiff ? COLOR_RARITY_PAIRS[lower].color : COLOR_RARITY_PAIRS[upper].color));
      continue;
    }
    const closestIndex = i - pivotIndices[lower] <= pivotIndices[upper] - i ? lower : upper;
    coloredMaps.add(toReelMap(map, COLOR_RARITY_PAIRS[closestIndex].color));
  }
  return coloredMaps;
}

export default {
  colorPlaylist(playlist: WorkshopPlaylistView) {
    const sortedPlaylist = playlist.getSorted({
      comparison: "weight",
      ascending: true,
    });
    const coloredMaps = assignColors(sortedPlaylist.filter(playlist => !playlist.hasPlayed));

    sortedPlaylist.filter(playlist => playlist.hasPlayed)
                  .forEach(map => coloredMaps.add(toReelMap(map, {color: 'white'})));
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

