<script setup lang="ts">

import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";

function createPlaylist() {
  RoutingService.navigateToCreateNewPlaylistPage();
}
</script>

<template>
  <RegButton @click="createPlaylist()">Create new playlist</RegButton>
</template>

<style scoped>

</style>
