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
import {usePlaylistStore} from "@/stores/spinner.store.ts";
import {useLoadingStore} from "@/stores/loading.store.ts";
import {InputTypes} from "@/models/input-types.ts";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";
import {useAddMapActions} from "@/composables/use-add-map-actions.ts";

const {workshopUrl, addMapToSelectedPlaylist} = useAddMapActions()

const emits = defineEmits<{
  "closeDialog": []
}>();

function addMap() {
  addMapToSelectedPlaylist().then(result => {
    if(!!result) {
      closeDialog();
    }
  })
}

function closeDialog() {
  emits('closeDialog');
}
</script>

<template>
  <StandardDialog header-text="Add new map...">
    <template v-slot:content>
      <div class="input-wrapper">
        <SingleLineTextField placeholder="Insert Workshop URL here"
                             :input-type="InputTypes.TEXT"
                             v-model="workshopUrl"
        />
        <ConfirmButton @click="addMap">Add</ConfirmButton>
        <RegButton @click="closeDialog">Cancel</RegButton>
      </div>
    </template>
  </StandardDialog>
</template>

<style scoped>
.input-wrapper {
  display: flex;
  gap: 0.5rem;
  justify-content: space-between;
}
</style>
