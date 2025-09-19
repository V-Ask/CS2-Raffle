<script setup lang="ts">
import {useRoute} from "vue-router";
import {computed, onMounted, ref} from "vue";
import {Reel} from "@/models/reel.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import SpinnerPreview from "@/components/spinner/preview/SpinnerPreview.vue";
import SpinningReel from "@/components/spinner/reel/SpinningReel.vue";
import {useSpinnerStore} from "@/stores/spinner.ts";

const spinnerStore = useSpinnerStore();

const id = getPlaylistId();
const reel = computed(() => {
  const selectedPlaylist = spinnerStore.selectedPlaylist;
  return selectedPlaylist ? new Reel(selectedPlaylist) : null;
})
if(id) {
  PlaylistService.selectPlaylist(id).then();
}


function getPlaylistId() {
  const route = useRoute();
  const id = route.query.id;
  if (!id) {
    console.warn("No playlist id found in the params. How are you here??");
  }
  if (Array.isArray(id)) {
    return id[0]?.toString();
  }
  return id?.toString();
}
</script>

<template>
  <div v-if="!reel">
    <p></p>
  </div>
  <div v-else>
    <SpinningReel :reel="reel" v-if="spinnerStore.isSpinning"></SpinningReel>
    <SpinnerPreview :colored-maps="reel.coloredMaps" v-else></SpinnerPreview>
  </div>
</template>

<style scoped>

</style>
