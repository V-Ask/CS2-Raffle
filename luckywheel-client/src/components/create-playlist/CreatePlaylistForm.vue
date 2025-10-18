<script setup lang="ts">

import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {useSpinnerStore} from "@/stores/spinner.store.ts";

const spinnerStore = useSpinnerStore();

const input = ref("");

function cancelCreate() {
  RoutingService.navigateToPlaylistSelection();
}

function createPlaylist() {
  spinnerStore.createPlaylist(input.value).then(playlist => {
    RoutingService.navigateToPlaylistPage(playlist.playlistId);
  });
}
</script>

<template>
  <SingleLineTextField :max-length="50" :v-model="input"/>
  <ConfirmButton @click="createPlaylist()">Create</ConfirmButton>
  <RegButton @click="cancelCreate()">Cancel</RegButton>
</template>

<style scoped>

</style>
