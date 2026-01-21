<script setup lang="ts">
import {GameHosts} from "@/models/game-hosts.ts";
import DathostForm from "@/components/dialogs/settings/game-hosts/DathostForm.vue";
import {computed, ref} from "vue";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";
import RegButton from "@/components/buttons/RegButton.vue";

const props = defineProps<{
  saveExistsOnType?: GameHosts,
}>();

const gameHostService = defineModel<GameHostService>('game-host');
const shouldSaveHostSettings = defineModel<boolean>('should-save-host');
const host = ref<GameHosts>(gameHostService.value?.getType() || GameHosts.DATHOST);
const overrideExistingHost = ref<boolean>(false);

const saveExistsOnSelectedType = computed(() => {
  return props.saveExistsOnType && props.saveExistsOnType === host.value;
})

function isDathostSelected() {
  return host.value === GameHosts.DATHOST;
}

function resetGameHost() {
  gameHostService.value = undefined;
}

function gameHostChanged(newGameHost: GameHostService) {
  gameHostService.value = newGameHost;
}
</script>

<template>
  <div class="setting-wrapper">
    <div class="selector-wrapper">
      <label for="game-host-selector">Select Your Game Host:</label>
      <select name="game hosts" id="game-host-selector" v-model="host" @change="resetGameHost()">
        <option :value="GameHosts.DATHOST">Dathost</option>
      </select>
    </div>
    <div v-if="saveExistsOnSelectedType && !overrideExistingHost">
      <p>Game host is already set on type</p>
      <RegButton @clicked="overrideExistingHost = !overrideExistingHost">Override?</RegButton>
    </div>
    <DathostForm v-else-if="isDathostSelected()"
                 @hostChanged="gameHostChanged($event)"
                 v-model:should-save-game-host-to-storage="shouldSaveHostSettings"
    />

  </div>
</template>

<style scoped>
label {
  font-size: 1rem;
}

.setting-wrapper {
  display: flex;
  gap: 1rem;
}

.selector-wrapper {
  display: flex;
  flex-direction: column;
}

select {
  height: 2rem;
}
</style>
