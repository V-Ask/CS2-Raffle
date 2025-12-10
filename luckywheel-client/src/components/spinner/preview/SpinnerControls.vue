<script setup lang="ts">

import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import {SpinnerStatus, useSpinnerStore} from "@/stores/spinner.store.ts";
import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";

const spinnerStore = useSpinnerStore();

function spinReel() {
  spinnerStore.spinnerStatus = SpinnerStatus.SPINNING;
}

function cancelSpin() {
  RoutingService.navigateToPlaylistSelection();
}

function addMap() {
  const playlist = spinnerStore.selectedPlaylist;
  if (playlist) {
    RoutingService.navigateToAddMapToPlaylistPage(playlist.playlistId)
  }
}
</script>

<template>
  <div class="control-group">
    <ConfirmButton @click="spinReel()">Spin</ConfirmButton>
    <RegButton @click="cancelSpin()">Close</RegButton>
    <ConfirmButton @click="addMap()">Add Map</ConfirmButton>
  </div>
</template>

<style scoped>
.control-group {
  display: flex;
  gap: 0.5rem;
}
</style>
