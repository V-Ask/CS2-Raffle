import API from "@/router"
import {EDIT_PLAYLIST_VIEW, PLAYLIST_VIEW} from "@/helpers/constants/routing.ts";

function navigateToPage(name: string, addedPath?: string): void {
  API.router.push({
    name,
    params: { id: addedPath }
  }).then();
}
function navigateToPlaylistSelection() {
  navigateToPage(PLAYLIST_VIEW);
}

function navigateToPlaylistEdit() {
  navigateToPage(EDIT_PLAYLIST_VIEW);
}

export default {
  navigateToPlaylistSelection,
  navigateToPlaylistEdit
}
