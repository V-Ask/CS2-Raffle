<script setup lang="ts">

import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import GameHostSetting from "@/components/dialogs/settings/GameHostSetting.vue";
import Divider from "@/components/dialogs/settings/Divider.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import {onMounted, ref} from "vue";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";
import {useGameHostStore} from "@/stores/game-host.store.ts";
import type {GameHosts} from "@/models/game-hosts.ts";
import GameHostCreatorService from "@/services/game-host/game-host-creator.service.ts";

const emits = defineEmits<{
  closeDialog: []
}>()

const gameHostStore = useGameHostStore();
const selectedGameHost = ref<GameHostService>();
const gameHostLoadedFromStore = ref<GameHosts | undefined>();
const shouldSaveHostSettings = ref<boolean>(false);

function saveAndClose() {
  if (selectedGameHost.value) {
    gameHostStore.setNewGameHost(selectedGameHost.value);
    if (shouldSaveHostSettings.value) {
      selectedGameHost.value.saveConfig();
    }
  }
  close();
}

function close() {
  emits("closeDialog");
}

onMounted(() => {
  if(!gameHostStore.selectedGameHostService) {
    gameHostStore.selectDefaultGameHost();
  } else {
    gameHostLoadedFromStore.value = gameHostStore.selectedGameHostService.getType();
  }
  selectedGameHost.value = gameHostStore.selectedGameHostService;
})
</script>

<template>
  <StandardDialog header-text="Settings">
    <div class="settings-wrapper">
      <div class="setting">
        <label id="game-host-label">Setup Game Host</label>
        <GameHostSetting labelId="game-host-label"
                         :save-exists-on-type="gameHostLoadedFromStore"
                         v-model:game-host="selectedGameHost"
                         v-model:should-save-host="shouldSaveHostSettings"
        ></GameHostSetting>
      </div>
      <Divider/>
      <div class="setting">
        <label>Verify Email</label>
      </div>
      <Divider/>
      <div class="buttons">
        <ConfirmButton @clicked="saveAndClose()">Save</ConfirmButton>
        <RegButton @clicked="close()">Cancel</RegButton>
        <RegButton>Log out</RegButton>
      </div>
    </div>
  </StandardDialog>
</template>

<style scoped>
.settings-wrapper {
  display: grid;
  gap: 1rem;
}

.setting {
  display: grid;
  grid-auto-flow: column;
  gap: 1rem;
  grid-template-columns: 1fr 1fr;
}

.buttons {
  display: flex;
  justify-content: flex-end;
  gap: 1rem;
}
</style>
