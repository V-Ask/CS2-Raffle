<script setup lang="ts">
import {GameHosts} from "@/models/game-hosts.ts";
import DathostForm from "@/components/dialogs/settings/game-hosts/DathostForm.vue";
import {ref} from "vue";
import type {GameHostService} from "@/services/game-host/game-host.service.ts";

const gameHostService = defineModel<GameHostService>('game-host');
const host = ref<GameHosts>(gameHostService.value?.getType() || GameHosts.DATHOST);

function isDathostSelected() {
  return host.value === GameHosts.DATHOST;
}

function resetGameHost() {
  gameHostService.value = undefined;
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
    <DathostForm v-if="isDathostSelected()"
                 v-model:game-host="gameHostService"
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
