import {defineStore} from "pinia";
import {computed, ref} from "vue";
import {ReelMap} from "@/models/reel-map.ts";

export const useSortingOptionsStore = defineStore("sortingOptions", () => {
  const showState = ref<MapShowStates>(MapShowStates.UNPLAYED);
  const sortingOption = ref<SortOptions>(SortOptions.RARITY);

  const reelMapComparator = computed(() => ReelMap.getComparator(sortingOption.value));
  const showStateLabel = computed(() => showStateLabels[showState.value]);
  const sortingOptionLabel = computed(() => sortOptionLabels[sortingOption.value]);

  return {showState, sortingOption, reelMapComparator, showStateLabel, sortingOptionLabel};
})

export enum MapShowStates {
  UNPLAYED = "UNPLAYED",
  PLAYED = "PLAYED",
  BOTH = "BOTH",
}

export enum SortOptions {
  RARITY = "RARITY",
  NAME = "NAME",
  RATING = "RATING",
}

export const showStateLabels: Record<MapShowStates, string> = {
  [MapShowStates.UNPLAYED]: "Unplayed",
  [MapShowStates.PLAYED]: "Played",
  [MapShowStates.BOTH]: "Both",
};

export const sortOptionLabels: Record<SortOptions, string> = {
  [SortOptions.RARITY]: "Rarity",
  [SortOptions.NAME]: "Name",
  [SortOptions.RATING]: "Rating",
};
