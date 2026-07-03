<script setup lang="ts">

import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import {InputTypes} from "@/models/input-types.ts";
import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";

const emits = defineEmits<{
  playlistCallback: [value: WorkshopPlaylistView | undefined]
}>();


const spinnerStore = useSpinnerStore();

const input = ref("");
const isLoading = ref(false);
const validName = ref(true);


function createPlaylist() {
  validName.value = isValidName();
  if (!validName.value || isLoading.value) return;
  isLoading.value = true;
  PlaylistService.createNewPlaylist(input.value).then(playlist => {
    emitPlaylist(playlist);
    isLoading.value = false;
    input.value = "";
  }, _ => emitPlaylist());
}

function isValidName() {
  return !!input.value && !doesPlaylistExist();
}

function doesPlaylistExist() {
  return spinnerStore.playlistWithNameExists(input.value);
}

function emitPlaylist(playlist?: WorkshopPlaylistView) {
  emits('playlistCallback', playlist);
}

function cancelCreate() {
  emitPlaylist();
}
</script>

<template>
  <div class="wrapper">
    <div class="form-wrapper">
      <div class="input-wrapper">
        <SingleLineTextField :max-length="50"
                             :disabled="isLoading"
                             :input-type="InputTypes.TEXT"
                             v-model="input"/>
        <ConfirmButton :disabled="isLoading" @click="createPlaylist()">Create</ConfirmButton>
        <RegButton :disabled="isLoading" @click="cancelCreate()">Cancel</RegButton>
      </div>
      <p :class="{ 'error-hidden': validName }" class="error text">Playlist with this name is
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
  gap: 16px;
}

.error.text {
  width: 100%;
  font-size: 0.875rem;
  margin: 0.5rem;
  color: red;
}

.error-hidden {
  opacity: 0;
}
</style>
