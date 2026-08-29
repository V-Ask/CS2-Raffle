import {http, HttpResponse} from "msw";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import {WorkshopPlaylistViewDto} from "@/api/dto/views/workshop-playlist-view-dto.ts";
import {WorkshopPlaylistMapViewDto} from "@/api/dto/views/workshop-playlist-map-view-dto.ts";
import {WorkshopMapDto} from "@/api/dto/workshop-map-dto.ts";

const successResponse = () => new HttpResponse(null, {status: 200});

export default [
  http.post('/api/register', () => {
    return successResponse();
  }),

  http.post('/api/login', () => {
    return successResponse();
  }),

  http.post('/logout', () => {
    return successResponse();
  }),

  http.get('/api/User/auth', () => {
    return new HttpResponse({
      email: 'mock@email.com',
      emailConfirmed: true,
    }, {status: 201});
  }),

  http.get('/api/Playlist/all', () => {
    return HttpResponse.json({
      workshopPlaylists: [
        new WorkshopPlaylistIndex("Collection 1", "id-1"),
        new WorkshopPlaylistIndex("Collection 2", "id-2"),
        new WorkshopPlaylistIndex("Collection 3", "id-3"),
        new WorkshopPlaylistIndex("Collection 4", "id-4"),
      ]
    })
  }),

  http.get('/api/Playlist/', ({request}) => {
    const url = new URL(request.url);
    const id = url.searchParams.get('workshopPlaylistId');

    const mockPlaylists: Record<string, WorkshopPlaylistViewDto> = {
      'id-1': new WorkshopPlaylistViewDto('Collection 1', 'id-1', [
        new WorkshopPlaylistMapViewDto('Dust II', 'https://placehold.co/200x120', 'Classic competitive map', 'map-id-1', 'id-1', 3, false),
        new WorkshopPlaylistMapViewDto('Mirage', 'https://placehold.co/200x120', 'Popular mid-centric map', 'map-id-2', 'id-1', 5, true),
        new WorkshopPlaylistMapViewDto('Inferno', 'https://placehold.co/200x120', 'Tight corridors and apartments', 'map-id-3', 'id-1', 2, false),
        new WorkshopPlaylistMapViewDto('Nuke', 'https://placehold.co/200x120', 'Vertical gameplay with two levels', 'map-id-4', 'id-1', 1, false),
      ]),
      'id-2': new WorkshopPlaylistViewDto('Collection 2', 'id-2', [
        new WorkshopPlaylistMapViewDto('Ancient', 'https://placehold.co/200x120', 'Temple-themed competitive map', 'map-id-5', 'id-2', 4, false),
        new WorkshopPlaylistMapViewDto('Anubis', 'https://placehold.co/200x120', 'Egyptian-themed bomb defusal', 'map-id-6', 'id-2', 2, true),
        new WorkshopPlaylistMapViewDto('Overpass', 'https://placehold.co/200x120', 'Urban map with a park area', 'map-id-7', 'id-2', 3, false),
      ]),
      'id-3': new WorkshopPlaylistViewDto('Collection 3', 'id-3', [
        new WorkshopPlaylistMapViewDto('Vertigo', 'https://placehold.co/200x120', 'Rooftop construction site', 'map-id-8', 'id-3', 2, false),
        new WorkshopPlaylistMapViewDto('Cache', 'https://placehold.co/200x120', 'Classic mid-heavy map', 'map-id-9', 'id-3', 5, false),
        new WorkshopPlaylistMapViewDto('Train', 'https://placehold.co/200x120', 'Railyard with long sightlines', 'map-id-10', 'id-3', 1, true),
        new WorkshopPlaylistMapViewDto('Cobblestone', 'https://placehold.co/200x120', 'Castle-themed outdoor map', 'map-id-11', 'id-3', 3, false),
        new WorkshopPlaylistMapViewDto('Tuscan', 'https://placehold.co/200x120', 'Italian village layout', 'map-id-12', 'id-3', 2, false),
      ]),
      'id-4': new WorkshopPlaylistViewDto('Collection 4', 'id-4', [
        new WorkshopPlaylistMapViewDto('Office', 'https://placehold.co/200x120', 'Hostage rescue in an office', 'map-id-13', 'id-4', 1, false),
        new WorkshopPlaylistMapViewDto('Italy', 'https://placehold.co/200x120', 'Classic hostage rescue map', 'map-id-14', 'id-4', 1, false),
      ]),
    };

    const playlist = id ? mockPlaylists[id] : undefined;
    if (!playlist) return new HttpResponse(null, {status: 404});
    return HttpResponse.json(playlist);
  }),

  http.put('/api/Dathost/select', () => {
    return successResponse();
  }),

  http.post('/api/Dathost/test', () => {
    return successResponse();
  }),

  http.post('/api/Playlist', () => {
    return HttpResponse.json(new WorkshopPlaylistView("id-5", "New Playlist", []));
  }),

  http.put('/api/Playlist', () => {
    return HttpResponse.json(new WorkshopMapDto('Added Map', 'https://placehold.co/200x120', 'Mock added map', 'map-id-new'));
  }),

]
