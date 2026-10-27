<script setup lang="ts">
import SettingsButton from "@/components/buttons/SettingsButton.vue";
import {useAuthStore} from "@/stores/auth.store.ts";
import {useLoadingStore} from "@/stores/loading.store.ts";
import LoadingDialog from "@/components/dialogs/LoadingDialog.vue";
import {ref, watch} from "vue";

const authStore = useAuthStore();
const loadingStore = useLoadingStore();

const isLoadingDialogShown = ref(false);
const loadingDialog = ref<HTMLDialogElement | null>(null);

watch(() => loadingStore.isLoading, (isLoading) => {
  if (isLoading) {
    if (!isLoadingDialogShown.value) {
      isLoadingDialogShown.value = true;
      loadingDialog.value?.showModal();
    }
  } else {
    if (isLoadingDialogShown.value) {
      isLoadingDialogShown.value = false;
      loadingDialog.value?.close();
    }
  }
});
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
  <dialog ref="loadingDialog">
    <LoadingDialog/>
  </dialog>
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

dialog {
  background: none;
  border: none;
  outline: none;
  overflow: hidden;
}
</style>
