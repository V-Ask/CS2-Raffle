import {http, HttpResponse} from "msw";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";
import {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";

const successResponse =() =>  new HttpResponse(null, { status: 200 });

export default [
  http.post('https://localhost:8080/register', () => {
    return successResponse();
  }),

  http.post('https://localhost:8080/login', () => {
    return successResponse();
  }),

  http.get('https://localhost:8080/auth', () => {
    return successResponse();
  }),

  http.get('https://localhost:8080/api/Playlist/all', () => {
    return HttpResponse.json([
      new WorkshopPlaylistIndex("Collection 1", "id-1"),
      new WorkshopPlaylistIndex("Collection 2", "id-2"),
      new WorkshopPlaylistIndex("Collection 3", "id-3"),
      new WorkshopPlaylistIndex("Collection 4", "id-4"),
    ]);
  }),

  // http.get('https://localhost:8080/api/Playlist', () => {
  //   return HttpResponse.json(new WorkshopPlaylistView("id-1", "Collection 1", [
  //     new WorkshopMap("Map 1", "map-id-1", 1, ""),
  //     new WorkshopMap("Map 2", "map-id-2", 5, ""),
  //     new WorkshopMap("Map 3", "map-id-3", 2, ""),
  //   ]))
  // }),

  http.post('https://localhost:8080/api/Playlist', () => {
    return HttpResponse.json(new WorkshopPlaylistView("id-5", "New Playlist", []));
  }),
  //
  // http.put('https://localhost:8080/api/Playlist/add', () => {
  //   return HttpResponse.json(new WorkshopMap("Added Map", "map-id-new", 1, ""));
  // }),

]
