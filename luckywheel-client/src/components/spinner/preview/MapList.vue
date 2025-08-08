<script setup lang="ts">
import {WorkshopMap} from "@/models/workshop-map.ts";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";

const props = defineProps<{
  workshopMaps: WorkshopMap[]
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
