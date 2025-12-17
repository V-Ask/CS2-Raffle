<script setup lang="ts">
import LoadingContainer from "@/components/loading/LoadingContainer.vue";
import {useLoadingStore} from "@/stores/loading.store.ts";
import {useAuthStore} from "@/stores/auth.store.ts";
import SettingsButton from "@/components/buttons/SettingsButton.vue";

const loadingStore = useLoadingStore();
const authStore = useAuthStore();
</script>

<template>
  <div id="main">
    <LoadingContainer :is-loading="loadingStore.isLazyLoading">
      <div class="content">
        <div class="toolbar-wrapper">
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
  height: 100%;
  width: 100%;
}
</style>
