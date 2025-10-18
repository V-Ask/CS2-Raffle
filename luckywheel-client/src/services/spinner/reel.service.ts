import type {Router} from "vue-router";
import {PLAYLIST_VIEW} from "@/helpers/constants/routing.ts";

function navigateToPlaylistSelector(router: Router) {
    return router.push({
        name: PLAYLIST_VIEW
    })
}

export default {
    navigateToPlaylistSelector
}
