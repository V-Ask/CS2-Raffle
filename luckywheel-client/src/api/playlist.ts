import {API} from "@/api/api.ts";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import type {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";

async function getAllPlaylistIndex(): Promise<WorkshopPlaylistIndex[]> {
  return API.get('/api/Playlist/all').then(response => {
    if (response.status === 200) {
      return response.data;
    }
    console.error(response);
  });
}

async function getPlaylist(id: string): Promise<WorkshopPlaylist> {
  return API.get(`/api/Playlist/`, {params: {workshopPlaylistId: id}}).then(response => {
    if (response.status === 200) {
      return response.data;
    }
    console.error(response);
  });
}

async function createPlaylist(name: string): Promise<WorkshopPlaylist> {
  return API.post('/api/Playlist', {}, {
    params: {
      collectionName: name,
    }
  }).then(response => {
    if (response.status === 200) {
      return response.data;
    }
    console.error(response);
  });
}

async function deletePlaylist(id: string): Promise<void> {
  return API.delete(`/api/Playlist/`, {params: {workshopPlaylistId: id}}).then(response => {
    if (response.status !== 200) {
      console.error(response);
    }
  });
}

async function addMapToPlaylist(playlistId: string, mapId: string): Promise<WorkshopMap> {
  return API.put('/api/Playlist/add', {}, {
    params: {
      workshopPlaylistId: playlistId,
      workshopMapId: mapId,
    },
  }).then(response => {
    if (response.status === 200) {
      return response.data;
    }
    console.error(response);
  });
}

export default {
  getAllPlaylistIndex,
  getPlaylist,
  deletePlaylist,
  addMapToPlaylist,
}
