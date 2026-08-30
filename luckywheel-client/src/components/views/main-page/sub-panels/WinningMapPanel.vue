<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import {computed, ref} from "vue";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";
import type {WinningMapActionCallback} from "@/models/winning-map-action-callback.ts";
import PlaylistService from "@/services/spinner/playlist.service.ts";
import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";
import PasswordPrompt from "@/components/dialogs/settings/game-hosts/PasswordPrompt.vue";
import ReadonlySingleLineTextField
  from "@/components/inputs/textfield/ReadonlySingleLineTextField.vue";
import {InputTypes} from "@/models/input-types.ts";

const props = defineProps<{
  winningMap: ReelMap,
  playlist: WorkshopPlaylistView
}>();

const emits = defineEmits<{
  cancelWinningMap: [Promise<void>]
}>();

const gameHostStore = useGameHostStore();

const incrementWeight = ref<boolean>(true);
const removeMap = ref<boolean>(false);
const passwordPrompt = ref<HTMLDialogElement | null>(null);
const isLaunchingMap = ref<boolean>(false);
const playError = ref<boolean>(false);

const canPlayMap = computed(() => {
  return gameHostStore.isGameHostSetup() && !isLaunchingMap.value;
})

const playMapButtonTooltip = computed(() => {
  if (!gameHostStore.isGameHostSetup()) {
    return "Game host is not configured. Please do so in the settings menu at the top-right."
  }
  if (isLaunchingMap.value) {
    return "Please wait while the map is being launched..."
  }
  return undefined;
})

function cancel() {
  emits('cancelWinningMap', Promise.resolve());
}

function playMap() {
  promptPassword();
}

function promptPassword() {
  passwordPrompt.value?.showModal()
}

async function onPasswordConfirmed() {
  passwordPrompt.value?.close();
  isLaunchingMap.value = true;
  const success = await gameHostStore.setWorkshopMap(props.winningMap);
  playError.value = !success;
  isLaunchingMap.value = false;
}

async function havePlayed() {
  if (incrementWeight.value) {
    emits('cancelWinningMap', props.playlist.incrementAllOtherMaps(props.winningMap, removeMap.value).then())
  } else if (removeMap.value) {
    emits('cancelWinningMap', props.playlist.removeMap(props.winningMap).then());
  }
}

function emptyCallback(): WinningMapActionCallback {
  return {
    shiftOtherWeights: false,
    removeMap: false,
  }
}
</script>

<template>
  <div class="wrapper">
    <div class="content-wrapper">
      <div class="winning-map-wrapper">
        <div class="icon-wrapper">
          <MapIcon :map="props.winningMap"/>
        </div>
        <ReadonlySingleLineTextField :text-field-value="`${props.winningMap.mapId}`"
                                     :input-type="InputTypes.TEXT"
                                     :show-copy-to-clipboard-button="true"/>
      </div>
      <div class="button-row">
        <ConfirmButton :disabled="!canPlayMap"
                       :title="playMapButtonTooltip"
                       @clicked="playMap()">
          Select map on Game Host
        </ConfirmButton>
        <RegButton
          @clicked="havePlayed()"
        >Have played?
        </RegButton>
        <RegButton
          @clicked="cancel()">Cancel
        </RegButton>


      </div>
      <div class="checkbox-col">
        <label>
          Increment other map's weights?
          <input type="checkbox"
                 v-model="incrementWeight"
          />
        </label>
        <label>
          Remove map from playlist?
          <input type="checkbox"
                 v-model="removeMap"
          />
        </label>
      </div>
      <p class="play-error" v-show="playError">
        There was an error playing the map. Please check your settings or manually insert the map
        using the above ID
        and press "Have played?" to apply settings.
      </p>
    </div>
  </div>
  <dialog ref="passwordPrompt">
    <PasswordPrompt @passwordSet="onPasswordConfirmed"/>
  </dialog>
</template>

<style scoped>
.wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
}

.content-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 1rem;
}

.icon-wrapper {
  width: 20rem;
  height: 11.33rem;
}

.button-row {
  display: flex;
  gap: 1rem;
}

.checkbox-col {
  display: flex;
  flex-direction: column;
  font-size: 0.8rem;
}

.play-error {
  width: 400px;
  color: red;
}

.winning-map-wrapper {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  width: 20rem;
}
</style>
