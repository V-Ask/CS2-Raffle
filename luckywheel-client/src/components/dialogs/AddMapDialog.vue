<script setup lang="ts">
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import WorkshopLinkService from "@/services/workshop/workshop-link.service.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import {useLoadingStore} from "@/stores/loading.store.ts";

const loadingStore = useLoadingStore();
const playlistStore = useSpinnerStore();

const props = defineProps<{
  playlist: WorkshopPlaylistView
}>();

const emits = defineEmits<{
  "addMap": [value: WorkshopMap],
  "closeDialog": []
}>();

const workshopUrl = ref("");
const isWorkshopUrlError = ref(false);

function returnToPlaylistView() {
  emits('closeDialog');
}

function addNewMap() {
  let id = WorkshopLinkService.getWorkshopId(workshopUrl.value);
  if (!id || mapAlreadyExists(id)) {
    console.log('wassup')
    isWorkshopUrlError.value = true;
    return;
  }
  const loadingCallback = loadingStore.startLoading();
  PlaylistService.addNewMapToPlaylist(id, props.playlist).then((map) => {
    loadingCallback();
    if (!map) {
      isWorkshopUrlError.value = true;
      return;
    }
    emits('addMap', map);
  }, err => {
    console.error('An error occured', err);
    loadingCallback();
  });
}

function mapAlreadyExists(mapId: string) {
  return playlistStore.getPlaylists.some((p) => p.playlistId === mapId);
}
</script>

<template>
  <StandardDialog header-text="Add new map...">
    <div class="input-wrapper">
      <div class="input">
        <SingleLineTextField placeholder="Insert Workshop URL here"
                             v-model="workshopUrl"></SingleLineTextField>
        <ConfirmButton @click="addNewMap()">Add</ConfirmButton>
        <RegButton @click="returnToPlaylistView()">Cancel</RegButton>
      </div>
      <p v-if="isWorkshopUrlError">URL is invalid or already exists.</p>
    </div>
  </StandardDialog>
</template>

<style scoped>
.input-wrapper {
  display: flex;
  flex-direction: column;
  height: 100px;
  color: red;
}

.input {
  display: flex;
  gap: 0.5rem;
}
</style>
