import {useSpinnerStore} from "@/stores/spinner.ts";
import PlaylistApi from "@/api/playlist-api.ts";
import {WorkshopPlaylist} from "@/models/workshop-playlist.ts";

async function updatePlaylistIndices() {
  const spinnerStore = useSpinnerStore();
  await PlaylistApi.getAllPlaylistIndex().then(results => {
    spinnerStore.playlistIndices = results;
  });
}

async function selectPlaylist(playlistId: string) {
  const spinnerStore = useSpinnerStore();
  const dto = await PlaylistApi.getPlaylist(playlistId);
  spinnerStore.selectedPlaylist = WorkshopPlaylist.fromDto(dto);
}

export default {
  updatePlaylistIndices,
  selectPlaylist,
}
