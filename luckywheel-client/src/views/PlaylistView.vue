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
  console.log('VAJ', spinnerStore.selectedPlaylist);
  const route = useRoute();
  const playlist = await spinnerStore.selectPlaylistFromParams(route);
  if (playlist) {
    reel.value = new Reel(playlist);
  }
}

function watchSpinningStatus() {
  watch(() => spinnerStore.spinnerStatus, (status) => {
    if (status === SpinnerStatus.SPINNING) {
      isSpinningReel.value = true;
      return;
    }
    isSpinningReel.value = false;
  });
}
</script>

<template>
  <div v-if="!reel">
    <p>Reel is loading...</p>
  </div>
  <div v-else>
    <SpinningReel :reel="reel" v-if="isSpinningReel"></SpinningReel>
    <SpinnerPreview :colored-maps="reel.coloredMaps" v-else></SpinnerPreview>
  </div>
</template>

<style scoped>

</style>
