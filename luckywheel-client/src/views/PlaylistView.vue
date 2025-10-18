<script setup lang="ts">
import {useRoute} from "vue-router";
import {ref, watch} from "vue";
import {Reel} from "@/models/reel.ts";
import SpinnerPreview from "@/components/spinner/preview/SpinnerPreview.vue";
import SpinningReel from "@/components/spinner/reel/SpinningReel.vue";
import {SpinnerStatus, useSpinnerStore} from "@/stores/spinner.store.ts";

const spinnerStore = useSpinnerStore();

const reel = ref<Reel | null>();
const isSpinningReel = ref<boolean>(false);

setupReel();
watchSpinningStatus();

async function setupReel() {
  const id = getPlaylistId();
  if(spinnerStore.selectedPlaylist?.playlistId === id) {
    reel.value = new Reel(spinnerStore.selectedPlaylist);
  } else {
    spinnerStore.selectPlaylist(id).then(playlist => {
      reel.value = new Reel(playlist);
    });
  }
}

function watchSpinningStatus() {
  watch(() => spinnerStore.spinnerStatus, (status) => {
    if(status === SpinnerStatus.SPINNING) {
      isSpinningReel.value = true;
      return;
    }
    isSpinningReel.value = false;
  });
}

function getPlaylistId() {
  const route = useRoute();
  const id = route.params.id;
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
    <SpinningReel :reel="reel" v-if="isSpinningReel"></SpinningReel>
    <SpinnerPreview :colored-maps="reel.coloredMaps" v-else></SpinnerPreview>
  </div>
</template>

<style scoped>

</style>
