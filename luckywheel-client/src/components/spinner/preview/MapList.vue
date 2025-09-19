<script setup lang="ts">
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import type {ReelMap} from "@/models/reel-map.ts";

const props = defineProps<{
  workshopMaps: ReelMap[]
}>();
</script>

<template>
<div class="wrapper">
  <div v-for="map in props.workshopMaps" class="icon">
    <MapIcon :map="map" />
  </div>
</div>
</template>

<style scoped>
.wrapper {
  min-width: 600px;
}
.icon {
  width: 100px;
  height: 80px;
}
</style>
