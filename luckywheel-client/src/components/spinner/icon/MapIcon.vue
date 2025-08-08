<script setup lang="ts">
import {WorkshopMap} from "@/models/workshop-map.ts";
import RarityBar from "@/components/spinner/icon/RarityBar.vue";

const props = defineProps<{
  map: WorkshopMap
}>();
</script>

<template>
    <div class="wrapper flex">
      <RarityBar :rarity="map.rarity"/>
      <div class="image-container">
      </div>
    </div>
    <span>{{ map.workshopName }}</span>
</template>

<style scoped>
.wrapper {
  height: 100%;
}
.image-container {
  background-color: grey;
}
</style>
