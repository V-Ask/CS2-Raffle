<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import {computed, onMounted, ref, watch} from "vue";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {InputTypes} from "@/models/input-types.ts";
import DropdownSelector from "@/components/inputs/DropdownSelector.vue";
import type {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinningReel from "@/components/spinner/reel/SpinningReel.vue";
import WinningMapComponent from "@/components/spinner/reel/WinningMapComponent.vue";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import ReelService from "@/services/spinner/reel.service.ts";
import type {ReelMap} from "@/models/reel-map.ts";
import type {WinningMapActionCallback} from "@/models/winning-map-action-callback.ts";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import CreatePlaylistDialog from "@/components/dialogs/CreatePlaylistDialog.vue";
import DialogService from "@/services/dialog.service.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import type {WorkshopMap} from "@/models/workshop-map.ts";
import AddMapDialog from "@/components/dialogs/AddMapDialog.vue";

enum ViewState { BROWSING, SPINNING, SPUN }

const createPlaylistDialog = ref<HTMLDialogElement | null>(null);
const addMapDialog = ref<HTMLDialogElement | null>(null);

const spinnerStore = useSpinnerStore();
const selectedPlaylistIndex = ref<WorkshopPlaylistIndex | null>(null);
const selectedPlaylist = ref<WorkshopPlaylistView>();
const coloredMaps = ref<Set<ReelMap>>(new Set());
const searchText = ref('');
const winnerMap = ref<ReelMap | null>(null);
const viewState = ref<ViewState>(ViewState.BROWSING);
const playlistIndices = ref<WorkshopPlaylistIndex[]>([]);
const isLoading = ref<boolean>(true);

onMounted(async () => {
  playlistIndices.value = await spinnerStore.updatePlaylistIndices();
  isLoading.value = false;
});

watch(selectedPlaylistIndex, async (newIndex) => {
  if (!newIndex) return;
  await resetPlaylist();
});

const filteredMaps = computed(() => {
  if (!searchText.value.trim()) return coloredMaps.value;
  const query = searchText.value.trim().toLowerCase();
  return new Set([...coloredMaps.value].filter(map => map.mapName.toLowerCase().includes(query)));
});

const playlistIndexOptionIndex = computed(() => (index: WorkshopPlaylistIndex) => index.playlistId);
const playlistIndexText = computed(() => (index: WorkshopPlaylistIndex) => index.collectionName);
const isBrowsing = computed(() => viewState.value === ViewState.BROWSING);
const playlistIsSelected = computed(() => !!selectedPlaylistIndex.value);
const playlistIsNotSelected = computed(() => !playlistIsSelected.value);

function startSpin() {
  viewState.value = ViewState.SPINNING;
}

function onMapSelected(map: ReelMap) {
  winnerMap.value = map;
  viewState.value = ViewState.SPUN;
}

async function onCancelWinner(onReady: Promise<void>) {
  isLoading.value = true;
  await onReady;
  await resetPlaylist();
  isLoading.value = false;
}

function onMapAdded(map: WorkshopMap) {
  if (selectedPlaylist.value) {
    selectedPlaylist.value.addNewMap(map);
    recolorMaps();
  }
}

function openCreatePlaylistDialog() {
  createPlaylistDialog.value?.showModal();
}

function closeCreatePlaylistDialog() {
  createPlaylistDialog.value?.close();
}

function handleBackdropClickCreatePlaylist(event: MouseEvent) {
  DialogService.handleBackdropClick(createPlaylistDialog.value!, event, () => closeCreatePlaylistDialog());
}

function openAddMapDialog() {
  addMapDialog.value?.showModal();
}

function closeAddMapDialog() {
  addMapDialog.value?.close();
}

function handleBackdropClickAddMap(event: MouseEvent) {
  DialogService.handleBackdropClick(addMapDialog.value!, event, () => closeAddMapDialog());
}

function handleCreateDialogClosed(playlistIndex?: WorkshopPlaylistIndex) {
  if (playlistIndex) {
    playlistIndices.value.push(playlistIndex);
  }
  closeCreatePlaylistDialog();
}

function recolorMaps() {
  if (selectedPlaylist.value) {
    coloredMaps.value = ReelService.colorPlaylist(selectedPlaylist.value);
  }
}

async function resetPlaylist() {
  if (selectedPlaylistIndex.value) {
    viewState.value = ViewState.BROWSING;
    winnerMap.value = null;
    const playlist = await PlaylistService.fetchPlaylistView(selectedPlaylistIndex.value.playlistId);
    if (playlist) {
      selectedPlaylist.value = playlist;
      recolorMaps();
    }
  }
}
</script>

<template>
  <div class="view-wrapper">
    <div class="bar-wrapper">
      <div class="select-wrapper" v-if="isBrowsing">
        <DropdownSelector v-model="selectedPlaylistIndex"
                          :disabled="isLoading"
                          :options="playlistIndices"
                          :option-index-fn="playlistIndexOptionIndex"
                          :option-text-fn="playlistIndexText">
          SELECT PLAYLIST
        </DropdownSelector>
        <button
            :disabled="isLoading"
            @click="openCreatePlaylistDialog">
          <i class="fa-solid fa-plus fa-2x add-button-offset"></i>
        </button>
      </div>
      <SingleLineTextField v-model="searchText"
                           placeholder="Search maps..."
                           :input-type="InputTypes.SEARCH"
                           :disabled="playlistIsNotSelected"
      />
    </div>
    <div class="map-wrapper">
      <div v-if="isLoading"
           class="loading-wrapper">
        <i class="fa-solid fa-spinner fa-spin fa-4x"></i>
      </div>
      <template v-else>
        <WinningMapComponent v-if="viewState === ViewState.SPUN && winnerMap && selectedPlaylist"
                             :winning-map="winnerMap"
                             :playlist="selectedPlaylist"
                             @cancelWinningMap="onCancelWinner($event)"/>
        <SpinningReel v-else-if="viewState === ViewState.SPINNING"
                      :reel-size="100"
                      :reel-maps="coloredMaps"
                      @mapSelected="onMapSelected($event)"/>
        <template v-else>
          <div v-if="coloredMaps.size" class="map-list-container">
            <MapList :workshop-maps="filteredMaps"/>
          </div>
          <p v-else-if="selectedPlaylist" class="state-message">Add a map to this playlist to get
            started!</p>
          <p v-else class="state-message">Select a playlist to preview its maps.</p>
        </template>
      </template>
    </div>
    <div class="bar-wrapper bottom-bar">
      <div class="map-controls" v-if="isBrowsing">

        <RegButton
            :disabled="playlistIsNotSelected"
            @click="openAddMapDialog"
        >
          ADD MAP
        </RegButton>
      </div>
      <ConfirmButton
          v-if="isBrowsing"
          :disabled="!coloredMaps.size || playlistIsNotSelected"
          @clicked="startSpin()">
        SPIN
      </ConfirmButton>
    </div>
  </div>
  <dialog ref="createPlaylistDialog" @click="handleBackdropClickCreatePlaylist">
    <CreatePlaylistDialog @dialogClosed="handleCreateDialogClosed($event)"/>
  </dialog>
  <dialog ref="addMapDialog" @click="handleBackdropClickAddMap">
    <AddMapDialog
        v-if="selectedPlaylist"
        @addMap="onMapAdded($event)"
        @closeDialog="closeAddMapDialog"
        :playlist="selectedPlaylist"
    ></AddMapDialog>
  </dialog>
</template>

<style scoped>
.view-wrapper {
  width: 100%;
  height: 100%;
  display: grid;
  grid-template-rows: auto 1fr auto;
}

.bar-wrapper {
  background: var(--top-bar-background);
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1rem 1.5rem;
}

.map-wrapper {
  background: var(--content-background);
  overflow: hidden;
}

.map-list-container {
  height: 100%;
  overflow: auto;
  padding: 1rem;
}

.state-message {
  color: #8a9bb0;
  text-align: center;
  padding-top: 3rem;
  letter-spacing: 0.05em;
}

.select-wrapper {
  display: flex;
  gap: 16px;
  width: 100%;
  height: 100%;
  align-items: stretch;
}

.map-controls {
  display: flex;
  gap: 16px;
}

.loading-wrapper {
  display: flex;
  height: 100%;
  justify-content: center;
  align-items: center;
}

.add-button-offset {
  margin-left: -6rem;
}
</style>
