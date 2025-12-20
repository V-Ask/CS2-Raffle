<script setup lang="ts">
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinnerControls from "@/components/spinner/preview/SpinnerControls.vue";
import {onMounted, ref} from "vue";
import {WorkshopMap} from "@/models/workshop-map.ts";
import RoutingService from "@/services/routing.service.ts";
import type {ReelMap} from "@/models/reel-map.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import ReelService from "@/services/spinner/reel.service.ts";
import DialogService from "@/services/dialog.service.ts";
import AddMapDialog from "@/components/dialogs/AddMapDialog.vue";

const props = defineProps<{
  playlist: WorkshopPlaylistView
}>();

const emits = defineEmits<{
  'spinReel': [value: Set<ReelMap>];
}>();

const addFormDialog = ref<HTMLDialogElement | null>(null)
const coloredMaps = ref<Set<ReelMap>>(new Set<ReelMap>());

onMounted(() => {
  updateColorPlaylist();
})

function addMap(map: WorkshopMap) {
  props.playlist.addNewMap(map);
  updateColorPlaylist();
  addFormDialog.value?.close();
}

function spinReel() {
  emits('spinReel', coloredMaps.value);
}

function returnToPlaylistSelection() {
  RoutingService.navigateToPlaylistSelection();
}

function updateColorPlaylist() {
  coloredMaps.value = ReelService.colorPlaylist(props.playlist);
}

function openDialog() {
  addFormDialog.value?.showModal();
}

function closeDialog() {
  addFormDialog.value?.close();
}

function handleBackdropClick(event: MouseEvent) {
  DialogService.handleBackdropClick(addFormDialog.value!, event, () => closeDialog());
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
  <dialog ref="addFormDialog" @click="handleBackdropClick($event)">
    <AddMapDialog @addMap="addMap($event)"
                  @closeDialog="closeDialog()"
                  :playlist="props.playlist"
    ></AddMapDialog>
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
