<script setup lang="ts">
import {ref} from "vue";
import PlaylistService from "@/services/spinner/playlist-service.ts";
import {useSpinnerStore} from "@/stores/spinner.ts";
import type {WorkshopPlaylistIndex} from "@/models/indices/workshop-playlist-index.ts";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";

const emits = defineEmits<{
  (e: 'playlistSelected', value: WorkshopPlaylistIndex): void,
}>();

const spinnerStore = useSpinnerStore();
const selected = ref<WorkshopPlaylistIndex | null>(null);
const playlistsLoaded = ref(false);
PlaylistService.updatePlaylistIndices().then(() => {
  playlistsLoaded.value = true;
});

function selectMap() {
  if(selected.value) {
    emits('playlistSelected', selected.value);
  }
}
</script>

<template>
<div v-if="playlistsLoaded">
  <select v-model="selected">
    <option disabled value="">Please select a map</option>
    <option v-for="index in spinnerStore.playlistIndices" :value="index">
      {{ index.collectionName }}
    </option>
  </select>
  <ConfirmButton :disabled="!!selected" @clicked="selectMap()">Select Playlist</ConfirmButton>
</div>
</template>

<style scoped>

</style>
