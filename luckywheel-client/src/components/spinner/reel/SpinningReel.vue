<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.ts";
import {ref, watch} from "vue";
import {WorkshopMap} from "@/models/workshop-map.ts";

const spinnerStore = useSpinnerStore();

const reelMaps = ref<WorkshopMap[]>([]);
const reelBuilt = ref<boolean>(false);

function buildReel() {
  if(spinnerStore.selectedPlaylist) {
    return spinnerStore.selectedPlaylist;
  }
  spinnerStore.watchSelectedPlaylist(())
}

function
</script>

<template>

</template>

<style scoped>

</style>
