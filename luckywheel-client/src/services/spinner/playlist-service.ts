import type {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import {useSpinnerStore} from "@/stores/spinner.ts";
import Playlist from "@/api/playlist.ts";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import {useRouter} from "vue-router";

async function updatePlaylistIndices() {
  const spinnerStore = useSpinnerStore();
  await Playlist.getAllPlaylistIndex().then(results => {
    spinnerStore.playlistIndices = results;
  });
}

function clearSelectedPlaylist() {
  const spinnerStore = useSpinnerStore();
  spinnerStore.selectedPlaylist =  null;
}

async function selectPlaylist(playlistIndex: WorkshopPlaylistIndex) {
  const spinnerStore = useSpinnerStore();
  await Playlist.getPlaylist(playlistIndex.playlistId).then(result => {
    spinnerStore.selectedPlaylist = result;
  });
}

export default {
  updatePlaylistIndices,
  clearSelectedPlaylist,
  selectPlaylist,
}
