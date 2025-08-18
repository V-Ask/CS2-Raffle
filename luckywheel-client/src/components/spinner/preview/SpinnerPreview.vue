<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.ts";
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinnerControls from "@/components/spinner/preview/SpinnerControls.vue";

const emits = defineEmits<{
  (e: 'playlistDismissed'): void,
  (e: 'playlistSpun'): void,
}>();
const spinnerStore = useSpinnerStore();
</script>
<template>
  <div v-if="spinnerStore.selectedPlaylist" class="playlist wrapper">
    <MapList :workshop-maps="spinnerStore.selectedPlaylist.maps"></MapList>
    <SpinnerControls @spin="emits('playlistSpun')"
                     @close="emits('playlistDismissed')" />
  </div>
  <div v-else class="loading wrapper">
    <p>Playlist is loading...</p>
  </div>
</template>
