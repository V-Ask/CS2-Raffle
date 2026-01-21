<script setup lang="ts">

import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";

const spinnerStore = useSpinnerStore();

const input = ref("");
const validName = ref(true);

function cancelCreate() {
  RoutingService.navigateToPlaylistSelection();
}

function createPlaylist() {
  validName.value = isValidName();
  if(!validName.value) return;
  PlaylistService.createNewPlaylist(input.value).then(playlist => {
    if (playlist) {
      RoutingService.navigateToPlaylistPage(playlist.playlistId);
    }
  });
}

function isValidName() {
  return !!input.value && !doesPlaylistExist();
}

function doesPlaylistExist() {
  return spinnerStore.playlistWithNameExists(input.value);
}
</script>

<template>
  <div class="wrapper">
    <div class="form-wrapper">
      <div class="input-wrapper">
        <SingleLineTextField :max-length="50" v-model="input"/>
        <ConfirmButton @click="createPlaylist()">Create</ConfirmButton>
        <RegButton @click="cancelCreate()">Cancel</RegButton>
      </div>
      <p :class="{ disabled: validName }" class="error text">Playlist with this name is
        unavailable</p>
    </div>
  </div>
</template>

<style scoped>
.wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
}

.form-wrapper {
  display: flex;
  width: fit-content;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.input-wrapper {
  display: flex;
}

.error.text {
  width: 100%;
  font-size: 0.875rem;
  margin: 0.5rem;
  color: red;
}

.disabled {
  opacity: 0;
}
</style>
