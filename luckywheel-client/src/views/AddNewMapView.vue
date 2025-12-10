<script setup lang="ts">

import AddMapForm from "@/components/add-playlist/AddMapForm.vue";
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import {ref} from "vue";
import {useRoute} from "vue-router";

const playlistId = ref("");

const spinnerStore = useSpinnerStore();

function fetchPlaylist() {
  const route = useRoute();
  spinnerStore.selectPlaylistFromParams(route).then(playlist => {
    if(playlist) {
      playlistId.value = playlist.playlistId
    }
  });
}

fetchPlaylist()
</script>

<template>
  <AddMapForm v-if="playlistId" :playlist-id="playlistId"></AddMapForm>
</template>

<style scoped>

</style>
