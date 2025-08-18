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
  }),
  getters: {
    getPlaylists: (state) => state.playlistIndices,
    getMapsFromSelectedPlaylist: (state) => state.maps
  },
  actions: {
    watchSelectedPlaylist(): Promise<WorkshopPlaylist | null> {
      return new Promise((resolve) => {
        watch(() => this.selectedPlaylist, (selectedPlaylist: WorkshopPlaylist | null) => {
          resolve(selectedPlaylist);
        });
      })
    }
  }
});
