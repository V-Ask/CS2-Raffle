import API from "@/router"
import {PLAYLIST_VIEW} from "@/helpers/constants/routing.ts";

function navigateToPage(name: string, addedPath?: string): void {
  API.router.push({
    name,
    params: { id: addedPath }
  }).then();
}
function navigateToPlaylistSelection() {
  navigateToPage(PLAYLIST_VIEW);
}
