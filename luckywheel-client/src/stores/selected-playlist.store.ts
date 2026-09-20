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


export const useSelectedPlaylistStore = defineStore('selectedPlaylist', () => {
  const playlistStore = usePlaylistStore();
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
  // True when a playlist is either not selected or empty
  const isPlaylistEmpty = computed(() => selectedPlaylist.value ? !selectedPlaylist.value.maps.length : true);

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
    if (!isSelectingPlaylist.value) {
      throw new Error("Failed to remove map. No playlist is selected.");
    }
    const stopLoadingCallback = loadingStore.startLoading();
    try {
      selectedPlaylist.value = await PlaylistService.removeMapFromPlaylist(map, selectedPlaylist.value!);
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
    selectedPlaylist,
    isPlaylistSelected,
    isPlaylistSelectedAndLoaded,
    selectPlaylistById,
    selectPlaylistByIndex,
    addMapToPlaylist,
    removeMapFromPlaylist,
    incrementAllMaps,
    isPlaylistEmpty,
    rateMap
  }
})
