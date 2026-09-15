<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import PasswordPrompt from "@/components/dialogs/settings/game-hosts/PasswordPrompt.vue";
import {computed, ref} from "vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";
import EditMapsDialog from "@/components/dialogs/map/EditMapsDialog.vue";
import ConfirmationDialog from "@/components/dialogs/ConfirmationDialog.vue";

const props = withDefaults(defineProps<{
  map: ReelMap,
  enableOverlayButtons?: boolean
}>(), {
  enableOverlayButtons: true
});

const gameHostStore = useGameHostStore();

const passwordPrompt = ref<HTMLDialogElement | null>(null);
const editMapsDialog = ref<HTMLDialogElement | null>(null);
const deleteConfirmDialog = ref<HTMLDialogElement | null>(null);
const isLaunchingMap = ref(false);
const color = computed(() => props.map.color);

function playMap() {
  passwordPrompt.value?.showModal();
}

function editMap() {
  editMapsDialog.value?.showModal();
}

function closeEdit() {
  editMapsDialog.value?.close();
}

function deleteMap() {
}

async function onPasswordConfirmed() {
  passwordPrompt.value?.close();
  isLaunchingMap.value = true;
  await gameHostStore.setWorkshopMap(props.map);
  isLaunchingMap.value = false;
}
</script>

<template>
  <div class="map-wrapper">
    <div class="map thumbnail">
      <img :src="map.imageUrl" :alt="`Thumbnail for ${map.mapName}`">
      <div v-if="props.enableOverlayButtons"
           class="overlay">
        <button class="play button"
                title="Play map"
                :class="{'launching-map': isLaunchingMap}"
                @click="playMap"
        >
          <i class="fa-solid fa-play fa-2x"></i>
        </button>
        <button class="edit button"
                title="Edit map"
                @click="editMap"
        >
          <i class="fa-solid fa-pen-to-square fa-2x"></i>
        </button>
        <button class="delete button" title="Remove map from playlist">
          <i class="fa-solid fa-trash fa-2x"></i>
        </button>
      </div>
    </div>
    <div class="map title-box">
      <p class="text" :title="props.map.mapName">
        {{ props.map.mapName }}
      </p>
    </div>
  </div>
  <dialog ref="editMapsDialog">
    <EditMapsDialog :map="props.map"
                    @close="closeEdit"
    />
  </dialog>
  <dialog ref="passwordPrompt">
    <PasswordPrompt @passwordSet="onPasswordConfirmed"/>
  </dialog>
  <dialog ref="deleteConfirmDialog">
    <ConfirmationDialog header-text="Are you sure you want to remove this map (can't be undone)?"
                        confirm-button-text="Yes"
                        cancel-button-text="No"
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

  .launching-map {
    background: gray;
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
  background-color: v-bind(color);
  overflow: hidden;
  padding: 0.5rem;
  height: 1rem;

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
