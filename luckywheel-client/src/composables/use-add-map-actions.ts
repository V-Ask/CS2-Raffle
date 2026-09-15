import {ref} from "vue";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";

export function useAddMapActions() {
  const selectedPlaylistStore = useSelectedPlaylistStore();

  const workshopUrl = ref("");
  const error = ref("");


  /**
   * Adds the map located at the currently written Steam Workshop URL to the selected playlist
   */
  async function addMapToSelectedPlaylist() {
    return selectedPlaylistStore.addMapToPlaylist(workshopUrl.value)
      .catch((err: Error) => {
        error.value = err.message;
        return undefined;
      })
  }

  return {
    workshopUrl,
    error,
    addMapToSelectedPlaylist
  }
}
