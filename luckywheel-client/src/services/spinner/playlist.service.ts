import PlaylistApi from "@/api/playlist-api.ts";
import {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";

async function fetchPlaylistIndices() {
  try {
    return await PlaylistApi.getAllPlaylistIndex();
  } catch (error) {
    console.error('Failed to fetch playlist indices:', error);
    throw error;
  }
}

async function fetchPlaylist(playlistId: string) {
  try {
    const dto = await PlaylistApi.getPlaylist(playlistId);
    return WorkshopPlaylist.fromDto(dto);
  } catch (error) {
    console.error('Failed to fetch playlist:', error);
    throw error;
  }
}

async function createNewPlaylist(name: string) {
  try {
    const dto = await PlaylistApi.createPlaylist(name);
    return WorkshopPlaylist.fromDto(dto);
  } catch (error) {
    console.error('Failed to create playlist:', error);
    throw error;
  }
}

async function addNewMapToPlaylist(workshopId: string, playlist: WorkshopPlaylist) {
  try {
    const dto = await PlaylistApi.addMapToPlaylist(playlist.playlistId, workshopId);
    return WorkshopMap.fromDto(dto);
  } catch (e) {
    console.error('Failed to add  map:', e)
    throw e;
  }
}

export default {
  fetchPlaylistIndices,
  fetchPlaylist,
  createNewPlaylist,
  addNewMapToPlaylist,
}
