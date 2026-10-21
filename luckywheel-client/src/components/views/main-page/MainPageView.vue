<script setup lang="ts">
import {usePlaylistStore} from "@/stores/spinner.store.ts";
import {computed, onMounted, ref} from "vue";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {InputTypes} from "@/models/input-types.ts";
import type {ReelMap} from "@/models/reel-map.ts";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import CreatePlaylistDialog from "@/components/dialogs/CreatePlaylistDialog.vue";
import DialogService from "@/services/dialog.service.ts";
import {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import AddMapDialog from "@/components/dialogs/AddMapDialog.vue";
import WinningMapPanel from "@/components/views/main-page/sub-panels/WinningMapPanel.vue";
import SpinningReelPanel from "@/components/views/main-page/sub-panels/SpinningReelPanel.vue";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";
import PlaylistDropdownSelector from "@/components/spinner/preview/PlaylistDropdownSelector.vue";
import {useLoadingStore} from "@/stores/loading.store.ts";
import BrowsePanel from "@/components/views/main-page/sub-panels/BrowsePanel.vue";
import SelectedPlaylistTopPanel from "@/components/top-panel/SelectedPlaylistTopPanel.vue";

enum ViewState { BROWSING, SPINNING, SPUN}

const createPlaylistDialog = ref<HTMLDialogElement | null>(null);
const addMapDialog = ref<HTMLDialogElement | null>(null);

const playlistStore = usePlaylistStore();
const selectedPlaylistStore = useSelectedPlaylistStore();
const loadingStore = useLoadingStore();
const searchText = ref('');
const winnerMap = ref<ReelMap | null>(null);
const viewState = ref<ViewState>(ViewState.BROWSING);

onMounted(async () => {
  const stopLoadingCallback = loadingStore.startLoading();
  await playlistStore.ensurePlaylistIndices();
  stopLoadingCallback();
});

const isBrowsing = computed(() => viewState.value === ViewState.BROWSING);

function startSpin() {
  viewState.value = ViewState.SPINNING;
}

function onMapSelected(map: ReelMap) {
  winnerMap.value = map;
  viewState.value = ViewState.SPUN;
}

function onMapDeselected() {
  winnerMap.value = null;
  viewState.value = ViewState.BROWSING;
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

function handleCreateDialogClosed() {
  closeCreatePlaylistDialog();
}
</script>

<template>
  <div class="view-wrapper">
    <div class="bar-wrapper">
      <template v-if="isBrowsing">
        <div class="select-wrapper">
          <PlaylistDropdownSelector/>
          <div class="playlist-buttons">
            <button
              title="Create new playlist"
              @click="openCreatePlaylistDialog"
            >
              <i class="fa-solid fa-plus fa-2x "></i>
            </button>
            <button
              title="Edit playlistIndices"
            >
              <i class="fa-solid fa-pen fa-xl"></i>
            </button>
          </div>
        </div>
        <SelectedPlaylistTopPanel
          v-if="viewState === ViewState.BROWSING && selectedPlaylistStore.isPlaylistSelectedAndLoaded"
          v-model="searchText"
        />
      </template>
    </div>
    <div class="map-wrapper">
      <WinningMapPanel v-if="viewState === ViewState.SPUN"
                       :winning-map="winnerMap!"
                       @cancelWinningMap="onMapDeselected"
      />
      <SpinningReelPanel v-else-if="viewState === ViewState.SPINNING"
                         :reel-size="100"
                         @mapSelected="onMapSelected($event)"
      />
      <BrowsePanel v-else-if="viewState === ViewState.BROWSING"
                   :search-string="searchText"
      />
    </div>
    <div class="bar-wrapper bottom-bar">
      <div class="map-controls" v-if="isBrowsing">
        <RegButton
          @click="openAddMapDialog"
        >
          ADD MAP
        </RegButton>
      </div>
      <ConfirmButton
        v-if="isBrowsing"
        :disabled="!selectedPlaylistStore.isPlaylistSelectedAndLoaded"
        @clicked="startSpin()">
        SPIN
      </ConfirmButton>
    </div>
  </div>
  <dialog ref="createPlaylistDialog" @click="handleBackdropClickCreatePlaylist">
    <CreatePlaylistDialog @dialogClosed="handleCreateDialogClosed"/>
  </dialog>
  <dialog ref="addMapDialog" @click="handleBackdropClickAddMap">
    <AddMapDialog
      @closeDialog="closeAddMapDialog"
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
  display: grid;
  grid-auto-flow: column;
  align-items: center;
  justify-content: space-between;
  padding: 1rem 1.5rem;
  min-height: 3rem;
}

.map-wrapper {
  background: var(--content-background);
  padding: 1rem 1.5rem;
  overflow: hidden;
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


.playlist-buttons {
  display: flex;
  align-items: center;
}
</style>
