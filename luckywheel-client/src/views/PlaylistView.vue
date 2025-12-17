<script setup lang="ts">
import {useRoute} from "vue-router";
import {ref} from "vue";
import SpinnerPreview from "@/components/spinner/preview/SpinnerPreview.vue";
import SpinningReel from "@/components/spinner/reel/SpinningReel.vue";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import ErrorComponent from "@/components/error-window/ErrorComponent.vue";
import {ReelMap} from "@/models/reel-map.ts";

const isLoading = ref<boolean>(true);
const selectedPlaylist = ref<WorkshopPlaylistView | undefined>(undefined);
const coloredMaps = ref<Set<ReelMap> | undefined>(undefined);
const winnerMap = ref<ReelMap | undefined>(undefined);

setupPlaylist();

async function setupPlaylist() {
  PlaylistService.fetchPlaylistFromRoute(useRoute()).then((response) => {
    isLoading.value = false;
    selectedPlaylist.value = response;
  });
}

function spinReel(maps: Set<ReelMap>) {
  coloredMaps.value = maps;
}

function selectMap(map: ReelMap) {
  winnerMap.value = map;
}
</script>

<template>
  <div v-if="isLoading">
    <p>Reel is loading...</p>
  </div>
  <div v-else-if="selectedPlaylist">
    <SpinningReel v-if="coloredMaps"
                  :reel-maps="coloredMaps"
                  :reel-size="100"
                  @mapSelected=""></SpinningReel>
    <SpinnerPreview :playlist="selectedPlaylist" @spinReel="spinReel($event)" v-else></SpinnerPreview>
  </div>
  <div v-else>
    <ErrorComponent></ErrorComponent>
  </div>
</template>

<style scoped>

</style>
