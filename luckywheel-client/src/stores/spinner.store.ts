import {defineStore} from "pinia";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import type {RouteLocationNormalizedLoaded} from "vue-router";
import {WorkshopPlaylistMapView} from "@/models/workshop-playlist-map-view.ts";

export const useSpinnerStore = defineStore('spinner', {
  state: () => ({
    playlistIndices: [] as WorkshopPlaylistIndex[],
    selectedPlaylist: null as WorkshopPlaylistView | null,
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
      let fetchedPlaylist = await PlaylistService.fetchPlaylistView(playlistId);
      if(!fetchedPlaylist) {
        return null;
      }
      this.selectedPlaylist = fetchedPlaylist;
      return this.selectedPlaylist;
    },

    async selectPlaylistFromParams(route: RouteLocationNormalizedLoaded<any>) {
      const playlistParamId = PlaylistService.getPlaylistIdQueryParam(route);
      if(playlistParamId === this.selectedPlaylist?.playlistId) {
        return this.selectedPlaylist;
      }
      return this.selectPlaylist(playlistParamId);
    },

    async createPlaylist(name: string) {
      const playlist = await PlaylistService.createNewPlaylist(name);
      if(!playlist) return;
      this.playlistIndices.push(WorkshopPlaylistIndex.fromPlaylist(playlist));
      return playlist;
    },

    async addWorkshopMapIdToSelectedPlaylist(workshopId: string) {
      if (!this.selectedPlaylist) {
        console.warn("Trying to add a map with no selected playlist...");
        return;
      }
      const newMap = await PlaylistService.addNewMapToPlaylist(workshopId, this.selectedPlaylist)
      console.log("newMap", newMap);
      if(!newMap) return;
      console.log("playlistMap", newMap.toPlaylistMap(this.selectedPlaylist.playlistId))
      this.selectedPlaylist.addMap(newMap.toPlaylistMap(this.selectedPlaylist.playlistId));
      return newMap;
    },
  }
});

export enum SpinnerStatus {
  VIEWING,
  SPINNING,
  SPUN
}
