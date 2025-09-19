import {type LocationQueryRaw, useRouter} from "vue-router";
import {
  CREATE_NEW_PLAYLIST_NAME,
  PLAYLIST_QUERY_PARAM,
  PLAYLIST_VIEW
} from "@/helpers/constants/routing.ts";

function navigateToPage(name: string, query?: LocationQueryRaw): void {
  const router = useRouter();
  router.push({
    name,
    query
  }).then();
}

function navigateToCreateNewPlaylistPage() {
  navigateToPage(CREATE_NEW_PLAYLIST_NAME);
}

function navigateToPlaylistSelection() {
  navigateToPage(PLAYLIST_VIEW);
}

function navigateToPlaylistPage(playlistId: string): void {
  navigateToPage(PLAYLIST_VIEW, {
    [PLAYLIST_QUERY_PARAM]: playlistId,
  });
}

export default {
  navigateToCreateNewPlaylistPage,
  navigateToPlaylistSelection,
  navigateToPlaylistPage,
}
