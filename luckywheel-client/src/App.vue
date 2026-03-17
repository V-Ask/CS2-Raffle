<script setup lang="ts">
import LoadingContainer from "@/components/loading/LoadingContainer.vue";
import {useLoadingStore} from "@/stores/loading.store.ts";
import {useAuthStore} from "@/stores/auth.store.ts";
import SettingsButton from "@/components/buttons/SettingsButton.vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";

const authStore = useAuthStore();
const gameHostStore = useGameHostStore();
</script>

<template>
  <div id="main">
    <LoadingContainer>
      <div class="content">
        <div class="toolbar-wrapper">
          <p class="warning-text"
             v-if="authStore.isLoggedIn && gameHostStore.hasFailedAndChecked()">
            Missing Game Host Setup. Please check your settings:
          </p>
          <SettingsButton v-if="authStore.isLoggedIn"/>
        </div>
        <RouterView/>
      </div>
    </LoadingContainer>
  </div>
</template>
<style scoped>
#main {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.content {
  flex: 1;
  margin: 1rem;
  display: grid;
  grid-auto-flow: row;
  grid-template-rows: 0fr 1fr;
  gap: 1rem;
}

.toolbar-wrapper {
  display: flex;
  align-items: center;
  justify-content: flex-end;
}

.warning-text {
  margin: 0.5rem;
  color: black;
}


</style>
