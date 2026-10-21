<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import PasswordPrompt from "@/components/dialogs/settings/game-hosts/PasswordPrompt.vue";
import {computed, ref} from "vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";
import EditMapsDialog from "@/components/dialogs/map/EditMapsDialog.vue";
import ConfirmationDialog from "@/components/dialogs/ConfirmationDialog.vue";
import {useDialogActions} from "@/composables/dialog-actions.ts";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";
import StarSummary from "@/components/spinner/icon/StarSummary.vue";
import {
  MapShowStates,
  SortOptions,
  useSortingOptionsStore
} from "@/stores/sorting-options.store.ts";

const props = withDefaults(defineProps<{
  map: ReelMap,
  enableOverlayButtons?: boolean
}>(), {
  enableOverlayButtons: true
});

const gameHostStore = useGameHostStore();
const selectedPlaylistStore = useSelectedPlaylistStore();
const sortingOptionsStore = useSortingOptionsStore();

const passwordPrompt = ref<HTMLDialogElement | null>(null);
const deleteConfirmDialog = ref<HTMLDialogElement | null>(null);
const editMapDialog = ref<HTMLDialogElement | null>(null);
const {
  openDialog: openPasswordPrompt,
  closeDialog: closePasswordPrompt,
  handleBackdropClick: handleBackdropClickPassword
} = useDialogActions(passwordPrompt);
const {
  openDialog: openEditDialog,
  closeDialog: closeEditDialog,
  handleBackdropClick: handleBackdropClickEdit,
  isDialogOpen: isEditOpen
} = useDialogActions(editMapDialog);
const {
  openDialog: openDeleteDialog,
  closeDialog: closeDeleteDialog,
  handleBackdropClick: handleBackdropClickDelete,
  isDialogOpen: isDeleteOpen
} = useDialogActions(deleteConfirmDialog);
const isLaunchingMap = ref(false);
const playError = ref(false);
const color = computed(() => {
  if (props.map.hasPlayed && sortingOptionsStore.showState === MapShowStates.BOTH) {
    return 'green';
  }
  return props.map.color
});

const canPlayMap = computed(() => {
  return gameHostStore.isGameHostSetup() && !isLaunchingMap.value;
});

const playMapButtonTooltip = computed(() => {
  if (!gameHostStore.isGameHostSetup()) {
    return "Game host is not configured. Please do so in the settings menu at the top-right."
  }
  if (isLaunchingMap.value) {
    return "Please wait while the map is being launched..."
  }
  if (playError.value) {
    return "There was an error playing the map. Please check your settings."
  }
  return "Play map";
});

function playMap() {
  if (!canPlayMap.value) return;
  openPasswordPrompt();
}

async function onPasswordConfirmed() {
  closePasswordPrompt();
  isLaunchingMap.value = true;
  const success = await gameHostStore.setWorkshopMap(props.map);
  playError.value = !success;
  isLaunchingMap.value = false;
}

async function onDeleteConfirmed() {
  try {
    await selectedPlaylistStore.removeMapFromPlaylist(props.map);
  } catch (e) {
    console.error(e);
  } finally {
    closeDeleteDialog();
  }
}

</script>

<template>
  <div class="map-wrapper">
    <div class="map thumbnail">
      <img :src="map.imageUrl" :alt="`Thumbnail for ${map.mapName}`">
      <div v-if="props.enableOverlayButtons"
           class="overlay">
        <button class="play button"
                :title="playMapButtonTooltip"
                :disabled="!canPlayMap"
                :class="{'launching-map': isLaunchingMap, 'play-error': playError}"
                @click="playMap"
        >
          <i class="fa-solid fa-play fa-2x"></i>
        </button>
        <button class="edit button"
                title="Edit map"
                @click="openEditDialog"
        >
          <i class="fa-solid fa-pen-to-square fa-2x"></i>
        </button>
        <button class="delete button"
                title="Remove map from playlist"
                @click="openDeleteDialog"
        >
          <i class="fa-solid fa-trash fa-2x"></i>
        </button>
      </div>
    </div>
    <div class="map title-box">
      <p class="text" :title="props.map.mapName">
        {{ props.map.mapName }}
      </p>
      <StarSummary v-if="!!props.map.rating"
                   :rating="props.map.rating"/>
    </div>
  </div>
  <dialog ref="editMapDialog" @click="handleBackdropClickEdit">
    <EditMapsDialog
      v-if="isEditOpen"
      :map="props.map"
      @close="closeEditDialog"
    />
  </dialog>
  <dialog ref="passwordPrompt" @click="handleBackdropClickPassword">
    <PasswordPrompt @passwordSet="onPasswordConfirmed"/>
  </dialog>
  <dialog ref="deleteConfirmDialog" @click="handleBackdropClickDelete">
    <ConfirmationDialog
      v-if="isDeleteOpen"
      header-text="Are you sure you want to remove this map (can't be undone)?"
      confirm-button-text="Yes"
      cancel-button-text="No"
      @confirm="onDeleteConfirmed"
      @cancel="closeDeleteDialog"
    />
  </dialog>
</template>

<style scoped>
.map-wrapper {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
  border: darkcyan solid 2px;
  overflow: hidden;
}

.map.thumbnail {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: black;
  flex: 1;
  min-height: 0;

  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
}

.overlay {
  position: absolute;
  inset: 0;
  background: rgba(0, 0, 0, 0.5);
  display: grid;
  grid-auto-flow: column;

  opacity: 0;
  visibility: hidden;
  transition: opacity 0.3s ease;

  .button {
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .button:disabled {
    background: rgba(128, 128, 128, 0.5);
    cursor: not-allowed;
  }

  .launching-map {
    background: gray;
  }

  .play-error {
    background: rgba(255, 0, 0, 0.5);
  }

  .play {
    background: rgba(8, 156, 0, 0.5);
  }

  .edit {
    background: rgba(255, 200, 0, 0.5);
  }

  .delete {
    background: rgba(255, 0, 0, 0.5);
  }
}

.map.thumbnail:hover .overlay {
  visibility: visible;
  opacity: 1;
}

.map.title-box {
  display: grid;
  grid-template-columns: minmax(0, 1fr) auto;
  align-items: center;
  gap: 0.5rem;

  background-color: v-bind(color);
  padding: 0.5rem;

  .text {
    margin: 0;
    line-height: 1;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    color: white;
  }
}
</style>
