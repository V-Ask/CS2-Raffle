<script setup lang="ts">

import MapList from "@/components/spinner/preview/MapList.vue";
import type {ReelMap} from "@/models/reel-map.ts";

const props = defineProps<{
  maps: Set<ReelMap>
}>();
</script>

<template class="map-list-container">
  <MapList :workshop-maps="maps"/>
</template>

<style scoped>
.map-list-container {
  height: 100%;
  overflow: auto;
  padding: 1rem;
}
</style>
