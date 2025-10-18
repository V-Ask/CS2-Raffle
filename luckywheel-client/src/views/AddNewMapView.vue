<script setup lang="ts">

import AddMapForm from "@/components/add-playlist/AddMapForm.vue";
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import {ref} from "vue";

const playlistId = ref("");

const spinnerStore = useSpinnerStore();

if(spinnerStore.selectedPlaylist) {
  playlistId.value = spinnerStore.selectedPlaylist.playlistId;
}
</script>

<template>
  <AddMapForm :playlist-id="playlistId"></AddMapForm>
</template>

<style scoped>

</style>
