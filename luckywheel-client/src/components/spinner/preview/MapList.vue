<script setup lang="ts">
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import type {ReelMap} from "@/models/reel-map.ts";

const props = defineProps<{
  workshopMaps: ReelMap[]
}>();
</script>

<template>
<div class="icon-wrapper">
  <div v-for="map in props.workshopMaps" class="icon">
    <MapIcon :map="map" />
  </div>
</div>
</template>

<style scoped>
.icon-wrapper {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
}

.icon {
  width: 12rem;
  height: 9.25rem;
}
</style>
