<script setup lang="ts">

import type {WorkshopMap} from "@/models/workshop-map.ts";
import {ref} from "vue";
import RegButton from "@/components/buttons/RegButton.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";

const props = defineProps<{
  map: WorkshopMap
}>();

const emits = defineEmits<{
  removeMap: [],
  incrementMapWeight: [],
  closePlayer: [shouldRemoveMap: boolean],
}>();

const shouldRemoveMapAfterPlaying = ref(false)
const shouldIncrementWeightAfterPlaying = ref(true);
const coolingDownPlayer = ref(false);

function closePlayer(shouldRemoveMap: boolean = false) {
  emits('closePlayer', shouldRemoveMap);
}

function playMap() {
  if(coolingDownPlayer.value) return;
  coolingDownPlayer.value = true;
  //TODO: Add logic to play map
  setTimeout(() => {
    coolingDownPlayer.value = false;
  }, 5000)
}
</script>

<template>
  <div class="player-wrapper">
    <ConfirmButton :disabled="coolingDownPlayer"
                   @clicked="playMap()">Play Map
    </ConfirmButton>
    <RegButton @clicked="closePlayer()">Cancel</RegButton>
    <RegButton @clicked="closePlayer(true)">Remove Map from Playlist</RegButton>
    <label>
      <input type="checkbox" v-model="shouldRemoveMapAfterPlaying"/>
      Remove map after playing
    </label>
    <label>
      <input type="checkbox" v-model="shouldIncrementWeightAfterPlaying"/>
      Increment weights after playing
    </label>
  </div>
</template>

<style scoped>

</style>
