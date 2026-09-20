import {defineStore} from "pinia";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";

export const usePlaylistStore = defineStore('spinner', {
  state: () => ({
    playlistIndices: [] as WorkshopPlaylistIndex[],
    playlistIndicesLoaded: false,
    playlistViewCache: new Map<string, WorkshopPlaylistView>(),
    spinnerStatus: SpinnerStatus.VIEWING as SpinnerStatus,
  }),
  getters: {
    getPlaylists: (state) => state.playlistIndices,
    isSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPINNING,
    isDoneSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPUN,
  },
  actions: {
    /**
     * Returns the cached playlist indices, only hitting the server the first time it's called.
     */
    async ensurePlaylistIndices() {
      if (!this.playlistIndicesLoaded) {
        await this.updatePlaylistIndices();
      }
      return this.playlistIndices;
    },

    async updatePlaylistIndices() {
      this.playlistIndices = await PlaylistService.fetchPlaylistIndices();
      this.playlistIndicesLoaded = true;
      return this.playlistIndices;
    },

    addPlaylistIndex(index: WorkshopPlaylistIndex) {
      this.playlistIndices.push(index);
    },

    playlistWithNameExists(name: string) {
      return this.playlistIndices.some(index => index.collectionName === name);
    },

    /**
     * Returns the cached playlist view for the given id, only hitting the server if it
     * hasn't been fetched before or a refresh is explicitly requested.
     */
    async getPlaylistView(playlistId: string, forceRefresh: boolean = false) {
      if (!forceRefresh && this.playlistViewCache.has(playlistId)) {
        return this.playlistViewCache.get(playlistId);
      }
      const playlist = await PlaylistService.fetchPlaylistView(playlistId);
      if (playlist) {
        this.playlistViewCache.set(playlistId, playlist);
      }
      return playlist;
    },

    updateViewCache(playlistView: WorkshopPlaylistView) {
      this.playlistViewCache.set(playlistView.playlistId, playlistView);
    }
  }
});

export enum SpinnerStatus {
  VIEWING,
  SPINNING,
  SPUN
}
