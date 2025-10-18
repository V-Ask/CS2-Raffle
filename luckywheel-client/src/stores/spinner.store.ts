import {defineStore} from "pinia";
import {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {warn, watch} from "vue";

export const useSpinnerStore = defineStore('spinner', {
  state: () => ({
    playlistIndices: [] as WorkshopPlaylistIndex[],
    selectedPlaylist: null as WorkshopPlaylist | null,
    spinnerStatus: SpinnerStatus.VIEWING as SpinnerStatus,
  }),
  getters: {
    getPlaylists: (state) => state.playlistIndices,
    getMapsFromSelectedPlaylist: (state) => state.selectedPlaylist?.maps,
    isSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPINNING,
    isDoneSpinning: (state) => state.spinnerStatus === SpinnerStatus.SPUN
  },
  actions: {
    async updatePlaylistIndices() {
      this.playlistIndices = await PlaylistService.fetchPlaylistIndices();
      return this.playlistIndices;
    },

    async selectPlaylist(playlistId: string) {
      this.selectedPlaylist = await PlaylistService.fetchPlaylist(playlistId);
      return this.selectedPlaylist;
    },

    async createPlaylist(name: string) {
      const playlist = await PlaylistService.createNewPlaylist(name);
      this.playlistIndices.push(WorkshopPlaylistIndex.fromDto(playlist));
      return playlist;
    },

    async addMapToSelectedPlaylist(workshopId: string) {
      if(!this.selectedPlaylist) {
        console.warn("Trying to add a map with no selected playlist...");
        return;
      }
      const newMap = await PlaylistService.addNewMapToPlaylist(workshopId, this.selectedPlaylist)
      this.selectedPlaylist.addMap(newMap);
      return newMap;
    },
  }
});

export enum SpinnerStatus {
  VIEWING,
  SPINNING,
  SPUN
}
