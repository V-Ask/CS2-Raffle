<script setup lang="ts">
import {ref} from "vue";
import {useSpinnerStore} from "@/stores/spinner.ts";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import RoutingService from "@/services/routing.service.ts";

const spinnerStore = useSpinnerStore();
const selected = ref<WorkshopPlaylistIndex | null>(null);
const playlistsLoaded = ref(false);
PlaylistService.updatePlaylistIndices().then(() => {
  playlistsLoaded.value = true;
});

function selectPlaylist() {
  if(selected.value) {
    RoutingService.navigateToPlaylistPage(selected.value.playlistId);
  }
}
</script>

<template>
<div v-if="playlistsLoaded">
  <select v-model="selected">
    <option v-for="index in spinnerStore.playlistIndices" :value="index" :name="index.collectionName">
      {{ index.collectionName }}
    </option>
  </select>
  <ConfirmButton @clicked="selectPlaylist()">Select Playlist</ConfirmButton>
</div>
</template>

<style scoped>

</style>
