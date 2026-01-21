<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";

const props = defineProps<{
  map: ReelMap
}>();

const color = props.map.color;
</script>

<template>
  <div class="map-wrapper">
    <div class="map thumbnail">
      <img :src="map.imageUrl" :alt="`Thumbnail for ${map.mapName}`">
    </div>
    <div class="map title-box">
      <p class="text" :title="props.map.mapName">
        {{ props.map.mapName }}
      </p>
    </div>
  </div>
</template>

<style scoped>
.map-wrapper {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  border: darkcyan solid 2px;
}

.map.thumbnail {
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: black;
  flex: 1;
  min-height: 0;

  img {
    max-width: 100%;
    max-height: 100%;
    width: auto;
    height: auto;
    object-fit: contain;
  }
}

.map.title-box {
  background-color: v-bind(color);
  overflow: hidden;
  padding: 0.5rem;
  height: 1rem;

  .text {
    margin: 0;
    line-height: 1;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }
}
</style>
