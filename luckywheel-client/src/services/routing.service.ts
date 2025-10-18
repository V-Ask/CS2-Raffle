import API from "@/router"
import {type LocationQueryRaw, useRouter} from "vue-router";
import {
  ADD_MAP_TO_PLAYLIST_NAME,
  CREATE_NEW_PLAYLIST_NAME,
  PLAYLIST_QUERY_PARAM, PLAYLIST_SELECTED_NAME,
  PLAYLIST_VIEW
} from "@/helpers/constants/routing.ts";

function navigateToPage(name: string, addedPath?: string): void {
  API.router.push({
    name,
    params: { id: addedPath }
  }).then();
}

function navigateToCreateNewPlaylistPage() {
  navigateToPage(CREATE_NEW_PLAYLIST_NAME);
}

function navigateToPlaylistSelection() {
  navigateToPage(PLAYLIST_VIEW);
}

function navigateToPlaylistPage(playlistId: string): void {
  navigateToPage(PLAYLIST_SELECTED_NAME, playlistId);
}

function navigateToAddMapToPlaylistPage(playlistId: string): void {
  navigateToPage(ADD_MAP_TO_PLAYLIST_NAME, playlistId);
}

export default {
  navigateToCreateNewPlaylistPage,
  navigateToPlaylistSelection,
  navigateToPlaylistPage,
  navigateToAddMapToPlaylistPage,
}
