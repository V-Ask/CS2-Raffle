<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {ref} from "vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import WorkshopLinkService from "@/services/workshop/workshop-link.service.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";

const props = defineProps<{
  playlist: WorkshopPlaylistView
}>();

const emits = defineEmits<{
  "addMap": [value: WorkshopMap],
  "closeDialog": []
}>();

const workshopUrl = ref("");
const isAddingMap = ref(false);

function returnToPlaylistView() {
  emits('closeDialog');
}

function addNewMap() {
  isAddingMap.value = true;
  let id = WorkshopLinkService.getWorkshopId(workshopUrl.value);
  if (!id) {
    console.warn("Invalid Workshop Link -- this needs clearer error reporting");
    return;
  }
  PlaylistService.addNewMapToPlaylist(id, props.playlist).then((map) => {
    if(!map) {
      console.warn("The map was not found. Try again.");
      isAddingMap.value = false;
      return;
    }
    isAddingMap.value = false;
    emits('addMap', map);
  });
}
</script>

<template>
  <div class="form-wrapper">
    <SingleLineTextField placeholder="Insert Workshop URL here"
                         v-model="workshopUrl"></SingleLineTextField>
    <ConfirmButton @click="addNewMap()">Add</ConfirmButton>
    <RegButton @click="returnToPlaylistView()">Cancel</RegButton>
  </div>
</template>

<style scoped>
.form-wrapper {
  display: flex;
  gap: 0.5rem;
}
</style>
