<script setup lang="ts">
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinnerControls from "@/components/spinner/preview/SpinnerControls.vue";
import AddMapForm from "@/components/add-playlist/AddMapForm.vue";
import {ref} from "vue";
import {WorkshopMap} from "@/models/workshop-map.ts";
import RoutingService from "@/services/routing.service.ts";
import type {ReelMap} from "@/models/reel-map.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";

const props = defineProps<{
  playlist: WorkshopPlaylistView
}>();

const emits = defineEmits<{
  'spinReel': [value: Set<ReelMap>];
}>();

const addFormDialog = ref<HTMLDialogElement | null>(null)
const coloredMaps = ref<Set<ReelMap>>(props.playlist.colorPlaylist());

function addMap(map: WorkshopMap) {
  props.playlist.addNewMap(map);
  addFormDialog.value?.close();
}

function spinReel() {
  emits('spinReel', coloredMaps.value);
}

function returnToPlaylistSelection() {
  RoutingService.navigateToPlaylistSelection();
}

function openDialog() {
  addFormDialog.value?.showModal();
}

function closeDialog() {
  addFormDialog.value?.close();
}

</script>
<template>
  <div class="preview-wrapper">
    <div class="map-list-container">
      <MapList :workshop-maps="coloredMaps"></MapList>
    </div>
    <div class="playlist controls">
      <SpinnerControls @addMap="openDialog()"
                       @cancelSpin="returnToPlaylistSelection()"
                       @spinReel="spinReel()"
      />
    </div>
  </div>
  <dialog ref="addFormDialog">
    <AddMapForm @addMap="addMap($event)"
                @onClose="closeDialog()"
                :playlist="props.playlist"
    ></AddMapForm>
  </dialog>
</template>
<style scoped>

.preview-wrapper {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  height: 100%;
  padding: 0 1rem;

  .map-list-container {
    flex-grow: 1;
    overflow: auto;
    border: black solid 4px;
  }

  .playlist.controls {
    display: flex;
    justify-content: flex-end;
  }
}
</style>
