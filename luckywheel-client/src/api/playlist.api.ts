import {API} from "@/api/api.ts";
import {WorkshopMapDto} from "@/api/dto/workshop-map-dto.ts";
import {GetUserPlaylistsResultDto} from "@/api/dto/get-user-playlists-result-dto.ts";
import type {WorkshopPlaylistViewDto} from "@/api/dto/views/workshop-playlist-view-dto.ts";
import type {
  CreateWorkshopPlaylistResultDto
} from "@/api/dto/create-workshop-playlist-result-dto.ts";

async function getAllPlaylistIndex(): Promise<GetUserPlaylistsResultDto> {
  return API.get('/api/Playlist/all').then(response => {
    console.log('response', response);
    if (response.status === 200) {
      return response.data;
    }
    console.error(response);
    return [];
  });
}

async function getPlaylistView(id: string): Promise<WorkshopPlaylistViewDto> {
  return API.get(`/api/Playlist/`, {
    params: {
      workshopPlaylistId: id,
    }
  }).then(response => {
    return response.data;
  }).catch(error => {
    console.error(error);
  })
}

async function createPlaylist(name: string): Promise<CreateWorkshopPlaylistResultDto> {
  return API.post('/api/Playlist', {
    collectionName: name,
  }).then(response => {
    return response.data;
  }).catch(error => {
    console.error(error);
  })
}

async function deletePlaylist(id: string): Promise<void> {
  return API.delete(`/api/Playlist/`, {params: {workshopPlaylistId: id}}).then(response => {
    if (response.status !== 200) {
      console.error(response);
    }
  });
}

async function addMapToPlaylist(playlistId: string, mapId: string): Promise<WorkshopMapDto> {
  return API.put('/api/Playlist', {
    collectionId: playlistId,
    workshopId: mapId,
  }).then(response => {
    return response.data;
  });
}

async function getMapFromPlaylist(playlistId: string, mapId: string): Promise<WorkshopMapDto> {
  return API.get('/api/Playlist/map', {
    params: {
      collectionId: playlistId,
      mapId,
    }
  }).then(response => {
    return response.data;
  })
}

async function removeMapFromPlaylist(playlistId: string, mapId: string): Promise<WorkshopPlaylistViewDto> {
  return API.delete('/api/Playlist/map', {
    params: {
      collectionId: playlistId,
      mapId
    }
  }).then(response => {
    return response.data;
  })
}

async function incrementAll(playlistId: string,
                            increment: number = 1,
                            exceptions: string[],
                            removeExceptions: boolean): Promise<void> {
  return API.post(`/api/Playlist/all/increase-weight`, {
    playlistId,
    increment,
    exceptions,
    removeExceptions
  }).then(response => {
    return response.data;
  })
}

export default {
  getAllPlaylistIndex,
  addMapToPlaylist,
  createPlaylist,
  getMapFromPlaylist,
  getPlaylistView,
  removeMapFromPlaylist,
  incrementAll
}
