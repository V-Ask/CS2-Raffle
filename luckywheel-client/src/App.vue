<script setup lang="ts">
import LoadingContainer from "@/components/loading/LoadingContainer.vue";
import SettingsButton from "@/components/buttons/SettingsButton.vue";
import {useAuthStore} from "@/stores/auth.store.ts";
import PasswordPrompt from "@/components/dialogs/settings/game-hosts/PasswordPrompt.vue";
import { onMounted } from "vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";

const authStore = useAuthStore();
</script>

<template>
  <div id="main">
      <div class="content">
        <div class="toolbar">
          <SettingsButton v-if="authStore.isLoggedIn"/>
        </div>
        <RouterView/>
      </div>
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
  height: 100%;
  width: 100%;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.toolbar {
  height: 60px;
  background: var(--toolbar-background);
  display: flex;
  align-items: center;
  justify-content: flex-end;
}

</style>
