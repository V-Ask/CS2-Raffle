<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";

const props = defineProps<{
  map: ReelMap
}>();

const color = props.map.color;
</script>

<template>
  <div class="map-box">
    {{ props.map.mapName }}
  </div>
</template>

<style scoped>
.map-box {
  background-color: v-bind(color);
}
</style>
