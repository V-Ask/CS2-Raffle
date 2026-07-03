<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import {computed} from "vue";

const props = defineProps<{
  map: ReelMap
}>();

const color = computed(() => props.map.color);
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
  box-sizing: border-box;
  border: darkcyan solid 2px;
  overflow: hidden;
}

.map.thumbnail {
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: black;
  flex: 1;
  min-height: 0;

  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
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
    color: white;
  }
}
</style>
