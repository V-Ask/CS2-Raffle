<script setup lang="ts">

import MapList from "@/components/spinner/preview/MapList.vue";
import type {ReelMap} from "@/models/reel-map.ts";
import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import {computed} from "vue";
import ReelService from "@/services/spinner/reel.service.ts";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";

const selectedPlaylistStore = useSelectedPlaylistStore();

const props = defineProps<{
  searchString: string
}>();

const filteredMaps = computed(() => {
  if (!props.searchString) return selectedPlaylistStore.sortedColoredMaps;
  const query = props.searchString.trim().toLowerCase();
  return selectedPlaylistStore.sortedColoredMaps.filter(map => map.mapName.toLowerCase().includes(query));
})
</script>

<template class="map-list-container">
  <MapList :workshop-maps="filteredMaps"/>
</template>

<style scoped>
.map-list-container {
  height: 100%;
  overflow: auto;
  padding: 1rem;
}
</style>
