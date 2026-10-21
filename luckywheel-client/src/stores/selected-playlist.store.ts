import {defineStore} from "pinia";
import {usePlaylistStore} from "@/stores/spinner.store.ts";
import {computed, ref} from "vue";
import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import WorkshopLinkService from "@/services/workshop/workshop-link.service.ts";
import {useLoadingStore} from "@/stores/loading.store.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import ReelService from "@/services/spinner/reel.service.ts";
import type {ReelMap} from "@/models/reel-map.ts";
import type {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import {MapShowStates, useSortingOptionsStore} from "@/stores/sorting-options.store.ts";


export const useSelectedPlaylistStore = defineStore('selectedPlaylist', () => {
  const playlistStore = usePlaylistStore();
  const sortingOptionsStore = useSortingOptionsStore();
  const loadingStore = useLoadingStore();


  // Whether currently fetching the playlist
  const isSelectingPlaylist = ref(false);
  // The currently selected playlist. If undefined, no playlist is currently selected
  const selectedPlaylist = ref<WorkshopPlaylistView>();

  // True when the playlist is selected or currently being selected
  const isPlaylistSelected = computed(() => isSelectingPlaylist.value || !!selectedPlaylist.value);
  // True when playlist is selected and loaded
  const isPlaylistSelectedAndLoaded = computed(() => !!selectedPlaylist.value)
  // The selected playlist's maps, colored by rarity
  const coloredMaps = computed(() => {
    if (!selectedPlaylist.value) {
      return new Set<ReelMap>();
    }
    return ReelService.colorPlaylist(selectedPlaylist.value);
  });
  // The selected playlist's maps, sorted by the selected sorting option
  const sortedColoredMaps = computed(() => {
    if (!selectedPlaylist.value) {
      return [];
    }
    return Array.from(coloredMaps.value).filter(map => {
      switch (sortingOptionsStore.showState) {
        case MapShowStates.UNPLAYED:
          return !map.hasPlayed;
        case MapShowStates.PLAYED:
          return map.hasPlayed;
        case MapShowStates.BOTH:
          return true;
      }
    }).sort(sortingOptionsStore.reelMapComparator);
  })
  // True when a playlist is either not selected or empty
  const isPlaylistEmpty = computed(() => selectedPlaylist.value ? !selectedPlaylist.value.maps.length : true);
  // True when the selected playlist along with the sorting options are empty or the playlist is not selected
  const isSortedPlaylistEmpty = computed(() => sortedColoredMaps.value ? !sortedColoredMaps.value.length : true);

  /**
   * Selects a playlist by its index
   * @param playlistIndex
   */
  async function selectPlaylistByIndex(playlistIndex: WorkshopPlaylistIndex) {
    return selectPlaylistById(playlistIndex.playlistId);
  }

  /**
   * Selects a playlist by its ID
   * @param playlistId
   */
  async function selectPlaylistById(playlistId: string) {
    isSelectingPlaylist.value = true;
    const playlist = await playlistStore.getPlaylistView(playlistId);
    selectedPlaylist.value = playlist;
    isSelectingPlaylist.value = false;
    return playlist;
  }

  /**
   * Adds a map to the selected playlist
   * @param mapUrl The Steam Workshop URL where the map is located
   */
  async function addMapToPlaylist(mapUrl: string) {
    if (!isPlaylistSelectedAndLoaded.value) {
      throw new Error("Playlist is not selected and loaded. This should be guarded against!");
    }
    let id = WorkshopLinkService.getWorkshopId(mapUrl);
    if (!isMapIdValid(id)) {
      throw new Error("Workshop ID invalid");
    }
    const stopLoadingCallback = loadingStore.startLoading();
    try {
      const newMap = await PlaylistService.addNewMapToPlaylist(id!, selectedPlaylist.value!);
      if (!newMap) {
        throw new Error("Failed to add map");
      }
      const playlist = selectedPlaylist.value!;
      playlist.addNewMap(newMap);
      playlistStore.playlistIndices
        .filter(index => index.playlistId === playlist.playlistId)
        .forEach(index => index.size = playlist.maps.length);
      return newMap;
    } finally {
      stopLoadingCallback();
    }
  }

  /**
   * Removes a map from the selected playlist
   * @param map Workshop Map to be removed
   */
  async function removeMapFromPlaylist(map: WorkshopMap) {
    if (!selectedPlaylist.value) {
      throw new Error("Failed to remove map. No playlist is selected.");
    }
    const stopLoadingCallback = loadingStore.startLoading();
    try {
      const playlist = selectedPlaylist.value;
      await PlaylistService.removeMapFromPlaylist(map, playlist);
      playlist.removeMapFromArray(map);
      playlistStore.playlistIndices
        .filter(index => index.playlistId === playlist.playlistId)
        .forEach(index => index.size = playlist.maps.length);
    } catch (e) {
      throw new Error("Failed to remove map: " + e,);
    } finally {
      stopLoadingCallback();
    }
  }

  /**
   * Increments the weight of all maps on the selected playlist
   */
  async function incrementAllMaps() {
    if (!selectedPlaylist.value) {
      throw new Error("Failed to increment weights. No playlist is selected.");
    }
    const stopLoadingCallback = loadingStore.startLoading();
    try {
      await PlaylistService.incrementAllMaps(selectedPlaylist.value!);
      selectedPlaylist.value!.incrementAllMaps();
    } catch (e) {
      throw new Error("Failed to increment all maps");
    } finally {
      stopLoadingCallback();
    }
  }

  /**
   * Rate map
   * @param map The workshop map to be rated
   * @param rating Rating between 0 and 5 (both inclusive)
   */
  async function rateMap(map: ReelMap, rating: number) {
    if (!selectedPlaylist.value) {
      throw new Error("Failed to rate map. No playlist is selected.");
    }
    const stopLoadingCallback = loadingStore.startLoading();
    try {
      await PlaylistService.rateMap(selectedPlaylist.value.playlistId, map.mapId, rating);
      selectedPlaylist.value.rateMap(map.toView(), rating);
    } catch (e) {
      throw new Error("Failed to rate map: " + e);
    } finally {
      stopLoadingCallback();
    }
  }

  /**
   * Mark map as played
   * @param map The workshop map to be marked
   * @param played Whether the map has been played
   */
  async function markMapAsPlayed(map: ReelMap, played: boolean = true) {
    if (!selectedPlaylist.value) {
      throw new Error("Failed to mark map as played. No playlist is selected.");
    }
    const stopLoadingCallback = loadingStore.startLoading();
    try {
      await PlaylistService.markMapAsPlayed(selectedPlaylist.value.playlistId, map.mapId, played);
      selectedPlaylist.value.markMapAsPlayed(map.mapId, played);
    } catch (e) {
      throw new Error("Failed to mark map as played: " + e);
    } finally {
      stopLoadingCallback();
    }
  }

  function isMapIdValid(id?: string) {
    return !!id && !!selectedPlaylist.value && !doesPlaylistContainId(id);
  }

  function doesPlaylistContainId(id: string) {
    return selectedPlaylist.value!
      .maps
      .map(mapView => mapView.mapId)
      .some(viewId => viewId === id);
  }

  return {
    coloredMaps,
    sortedColoredMaps,
    selectedPlaylist,
    isPlaylistSelected,
    isPlaylistSelectedAndLoaded,
    selectPlaylistById,
    selectPlaylistByIndex,
    addMapToPlaylist,
    removeMapFromPlaylist,
    incrementAllMaps,
    isPlaylistEmpty,
    isSortedPlaylistEmpty,
    rateMap,
    markMapAsPlayed
  }
})
