import {defineStore} from "pinia";
import {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import {watch} from "vue";

export const useSpinnerStore = defineStore('spinner', {
  state: () => ({
    playlistIndices: [] as WorkshopPlaylistIndex[],
    maps: [] as WorkshopMap[],
    selectedPlaylist: null as WorkshopPlaylist | null,
    spinnerStatus: SpinnerStatus.VIEWING as SpinnerStatus,
  }),
  getters: {
    getPlaylists: (state) => state.playlistIndices,
    getMapsFromSelectedPlaylist: (state) => state.maps,
    getSelectedPlaylist: (state) => state.selectedPlaylist,
    isSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPINNING,
    isDoneSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPUN
  },
  actions: {}
});

enum SpinnerStatus {
  VIEWING,
  SPINNING,
  SPUN
}
