<script setup lang="ts">

import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import GameHostSetting from "@/components/dialogs/settings/GameHostSetting.vue";
import Divider from "@/components/dialogs/settings/Divider.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import {computed, onMounted, ref} from "vue";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";
import {useGameHostStore} from "@/stores/game-host.store.ts";
import type {GameHosts} from "@/models/game-hosts.ts";
import GameHostCreatorService from "@/services/game-host/game-host-creator.service.ts";
import type {GameHostStoreService} from "@/models/game-host-store-service.ts";
import UserAuthService from "@/services/user-auth.service.ts";

const emits = defineEmits<{
  closeDialog: []
}>()

const gameHostStore = useGameHostStore();
const selectedGameHost = ref<GameHostStoreService>(gameHostStore.ensureGameHostService);
const shouldSaveHostSettings = ref<boolean>(false);
const gameHostLoadedFromStore = computed(() => {
  if(selectedGameHost.value.loadedFromStorage) {
    return selectedGameHost.value.service.getType();
  }
})

function saveAndClose() {
  if (selectedGameHost.value) {
    gameHostStore.setNewGameHost(selectedGameHost.value.service);
    if (shouldSaveHostSettings.value) {
      selectedGameHost.value.service.saveConfig();
    }
  }
  close();
}

function close() {
  emits("closeDialog");
}

</script>

<template>
  <StandardDialog header-text="Settings">
    <div class="settings-wrapper">
      <div class="setting">
        <label id="game-host-label">Setup Game Host</label>
        <GameHostSetting labelId="game-host-label"
                         :save-exists-on-type="gameHostLoadedFromStore"
                         v-model:game-host="selectedGameHost.service"
                         v-model:should-save-host="shouldSaveHostSettings"
        ></GameHostSetting>
      </div>
      <Divider/>
      <div class="buttons">
        <ConfirmButton @clicked="saveAndClose()">Save</ConfirmButton>
        <RegButton @clicked="close()">Cancel</RegButton>
        <RegButton @clicked="UserAuthService.logoutUser()">Log out</RegButton>
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
