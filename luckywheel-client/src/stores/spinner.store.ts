import {defineStore} from "pinia";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import type {RouteLocationNormalizedLoaded} from "vue-router";
import type {ReelMap} from "@/models/reel-map.ts";

export const useSpinnerStore = defineStore('spinner', {
  state: () => ({
    playlistIndices: [] as WorkshopPlaylistIndex[],
    spinnerStatus: SpinnerStatus.VIEWING as SpinnerStatus,
    selectedMap: null as ReelMap | null,
  }),
  getters: {
    getPlaylists: (state) => state.playlistIndices,
    isSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPINNING,
    isDoneSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPUN,
  },
  actions: {
    async updatePlaylistIndices() {
      this.playlistIndices = await PlaylistService.fetchPlaylistIndices();
      return this.playlistIndices;
    },

    playlistWithNameExists(name: string) {
      return this.playlistIndices.some(index => index.collectionName === name);
    }
  }
});

function playlistContainsReelMap(playlist: WorkshopPlaylistView, reelMap: ReelMap) {
  return playlist.maps.map(map => map.mapId).includes(reelMap.workshopId);
}

export enum SpinnerStatus {
  VIEWING,
  SPINNING,
  SPUN
}
