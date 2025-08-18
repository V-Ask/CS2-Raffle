<script setup lang="ts">

import PlaylistSelection from "@/components/inputs/PlaylistSelection.vue";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import {ref} from "vue";
import PlaylistService from "@/services/spinner/playlist-service.ts";
import SpinnerPreview from "@/components/spinner/preview/SpinnerPreview.vue";

const playlistSelected = ref<boolean>(false);
const wheelSpun = ref<boolean>(false);

function onPlaylistSelected(playlistIndex: WorkshopPlaylistIndex) {
  playlistSelected.value = true;
  PlaylistService.selectPlaylist(playlistIndex);
}

function onPlaylistDeselected() {
  playlistSelected.value = false;
  PlaylistService.clearSelectedPlaylist();
}

function onWheelSpun() {
  wheelSpun.value = true;
}
</script>

<template>
  <div v-if="wheelSpun" class="play post-selection">

  </div>
  <div v-else class="play pre-selection">
    <PlaylistSelection @playlistSelected="onPlaylistSelected($event)" />
    <SpinnerPreview @playlistSpun="onWheelSpun()"
                    @playlistDismissed="onPlaylistDeselected()" />
  </div>
</template>

<style scoped>

</style>
