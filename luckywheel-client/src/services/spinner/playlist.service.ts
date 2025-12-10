import PlaylistApi from "@/api/playlist-api.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";
import type {RouteLocationNormalizedLoaded} from "vue-router";
import {WorkshopPlaylistViewDto} from "@/api/dto/views/workshop-playlist-view-dto.ts";

async function fetchPlaylistIndices() {
  try {
    return PlaylistApi.getAllPlaylistIndex().then(results => {
      return results.workshopPlaylists
    });
  } catch (error) {
    console.error('Failed to fetch playlist indices:', error);
    return [];
  }
}

async function fetchPlaylistView(playlistId: string) {
  try {
    const dto = await PlaylistApi.getPlaylistView(playlistId);
    return WorkshopPlaylistView.fromDto(dto);
  } catch (error) {
    console.error('Failed to fetch playlist view:', error);
  }
}

async function createNewPlaylist(name: string) {
  try {
    const dto = await PlaylistApi.createPlaylist(name);
    return new WorkshopPlaylistView(dto.id, name, []);
  } catch (error) {
    console.error('Failed to create playlist:', error);
  }
}

async function addNewMapToPlaylist(workshopId: string, playlist: WorkshopPlaylistView) {
  try {
    const dto = await PlaylistApi.addMapToPlaylist(playlist.playlistId, workshopId);
    return WorkshopMap.fromDto(dto);
  } catch (e) {
    console.error('Failed to add  map:', e)
  }
}

function getPlaylistIdQueryParam(route: RouteLocationNormalizedLoaded<any>): string {
  const id = route.params.id;
  if (!id) {
    console.warn("No playlist id found in the params. How are you here??");
  }
  if (Array.isArray(id)) {
    return id[0]?.toString()
  }
  return id?.toString();
}

export default {
  fetchPlaylistIndices,
  createNewPlaylist,
  addNewMapToPlaylist,
  getPlaylistIdQueryParam,
  fetchPlaylistView
}
