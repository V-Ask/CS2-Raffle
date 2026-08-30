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
      <div class="overlay">
        <div class="play button">
          <button>
            <i class="fa-solid fa-play fa-2x"></i>
          </button>
        </div>
        <button class="edit button">
          <i class="fa-solid fa-pen-to-square fa-2x"></i>
        </button>
        <button class="delete button">
          <i class="fa-solid fa-trash fa-2x"></i>
        </button>
      </div>
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
  position: relative;
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

.overlay {
  position: absolute;
  inset: 0;
  background: rgba(0, 0, 0, 0.5);
  display: grid;
  grid-auto-flow: column;

  opacity: 0;
  visibility: hidden;
  transition: opacity 0.3s ease;

  .button {
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .play {
    background: rgba(8, 156, 0, 0.5);
  }

  .edit {
    background: rgba(255, 200, 0, 0.5);
  }

  .delete {
    background: rgba(255, 0, 0, 0.5);
  }
}

.map.thumbnail:hover .overlay {
  visibility: visible;
  opacity: 1;
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
