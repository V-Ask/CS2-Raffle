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
import {InputTypes} from "@/models/input-types.ts";

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
const isAddingMap = ref(false);

function returnToPlaylistView(map?: WorkshopMap) {
  if(map) {
    emits('addMap', map);
  }
  emits('closeDialog');
  workshopUrl.value = "";
  isWorkshopUrlError.value = false;
}

function addNewMap() {
  isAddingMap.value = true;
  let id = WorkshopLinkService.getWorkshopId(workshopUrl.value);
  if (!id || mapAlreadyExists(id)) {
    isWorkshopUrlError.value = true;
    return;
  }
  const loadingCallback = loadingStore.startLoading();
  PlaylistService.addNewMapToPlaylist(id, props.playlist).then((map) => {
    loadingCallback();
    if (!map) {
      isWorkshopUrlError.value = true;
      console.warn("The map was not found. Try again.");
      isAddingMap.value = false;
      return;
    }
    returnToPlaylistView(map);
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
      <SingleLineTextField placeholder="Insert Workshop URL here"
                           :input-type="InputTypes.TEXT"v-model="workshopUrl"></SingleLineTextField>
      <ConfirmButton @click="addNewMap()">Add</ConfirmButton>
      <RegButton @click="returnToPlaylistView()">Cancel</RegButton>
    </div>
  </StandardDialog>
</template>

<style scoped>
.input-wrapper {
  display: flex;
  gap: 0.5rem;
  justify-content: space-between;
}
</style>
