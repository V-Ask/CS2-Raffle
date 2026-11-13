import {API} from "@/api/api.ts";
import {WorkshopMapDto} from "@/api/dto/workshop-map-dto.ts";
import {GetUserPlaylistsResultDto} from "@/api/dto/get-user-playlists-result-dto.ts";
import type {WorkshopPlaylistViewDto} from "@/api/dto/views/workshop-playlist-view-dto.ts";
import type {
  CreateWorkshopPlaylistResultDto
} from "@/api/dto/create-workshop-playlist-result-dto.ts";


export default {
  getAllPlaylistIndex(): Promise<GetUserPlaylistsResultDto> {
    return API.get('/api/Playlist/all').then(response => {
      console.log('response', response);
      if (response.status === 200) {
        return response.data;
      }
      console.error(response);
      return [];
    });
  },

  getPlaylistView(id: string): Promise<WorkshopPlaylistViewDto> {
    return API.get(`/api/Playlist/`, {
      params: {
        workshopPlaylistId: id,
      }
    }).then(response => {
      return response.data;
    }).catch(error => {
      console.error(error);
    })
  },

  createPlaylist(name: string): Promise<CreateWorkshopPlaylistResultDto> {
    return API.post('/api/Playlist', {
      collectionName: name,
    }).then(response => {
      return response.data;
    }).catch(error => {
      console.error(error);
    })
  },

  async deletePlaylist(id: string): Promise<void> {
    return API.delete(`/api/Playlist/`, {params: {workshopPlaylistId: id}}).then(response => {
      if (response.status !== 200) {
        console.error(response);
      }
    });
  },

  async addMapToPlaylist(playlistId: string, mapId: string): Promise<WorkshopMapDto> {
    const response = await API.put('/api/Playlist', {
      collectionId: playlistId,
      workshopId: mapId,
    });
    return response.data;
  },

  async removeMapFromPlaylist(playlistId: string, mapId: string): Promise<void> {
    await API.delete('/api/Playlist/map', {
      params: {
        collectionId: playlistId,
        mapId
      }
    });
  },

  async incrementAll(playlistId: string,
                     increment: number = 1,
                     exceptions: string[],
                     removeExceptions: boolean): Promise<void> {
    const response = await API.post(`/api/Playlist/all/increase-weight`, {
      playlistId,
      increment,
      exceptions,
      removeExceptions
    });
    return response.data;
  },

  async rateMap(playlistId: string, mapId: string, rating: number): Promise<void> {
    const response = await API.put(`/api/Playlist/rate-map`, {
      playlistId,
      mapId,
      rating
    });
    return response.data;
  },

  async markMapAsPlayed(playlistId: string, mapId: string, played: boolean): Promise<void> {
    const response = await API.put(`/api/Playlist/played`, {
      playlistId,
      mapId,
      played
    });
    return response.data;
  }

}
