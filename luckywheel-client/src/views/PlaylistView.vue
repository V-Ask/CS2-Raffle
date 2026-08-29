<script setup lang="ts">
import {useRoute} from "vue-router";
import {ref} from "vue";
import SpinnerPreview from "@/components/spinner/preview/SpinnerPreview.vue";
import SpinningReel from "@/components/spinner/reel/SpinningReel.vue";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import ErrorComponent from "@/components/error-window/ErrorComponent.vue";
import {ReelMap} from "@/models/reel-map.ts";
import WinningMapComponent from "@/components/spinner/reel/WinningMapComponent.vue";
import type {WinningMapActionCallback} from "@/models/winning-map-action-callback.ts";

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

function deselectMap(winningCallback: WinningMapActionCallback) {
  winnerMap.value = undefined;
  coloredMaps.value = undefined;
}

function winnerMapDefined() {
  return !!winnerMap.value;
}
</script>

<template>
  <div v-if="isLoading">
    <p>Reel is loading...</p>
  </div>
  <div v-else-if="selectedPlaylist">
    <WinningMapComponent v-if="winnerMapDefined()" :winning-map="winnerMap!" @cancelWinningMap="deselectMap($event)"/>
    <SpinningReel v-else-if="coloredMaps"
                  :reel-maps="coloredMaps"
                  :reel-size="100"
                  @mapSelected="selectMap($event)"></SpinningReel>
    <SpinnerPreview :playlist="selectedPlaylist" @spinReel="spinReel($event)" v-else></SpinnerPreview>
  </div>
  <div v-else>
    <ErrorComponent></ErrorComponent>
  </div>
</template>

<style scoped>

</style>
