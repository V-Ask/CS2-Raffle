<script setup lang="ts">
import LoadingContainer from "@/components/loading/LoadingContainer.vue";
import {useLoadingStore} from "@/stores/loading.store.ts";

const loadingStore = useLoadingStore();
</script>

<template>
  <div id="main">
    <LoadingContainer :is-loading="loadingStore.isLazyLoading">
      <RouterView />
    </LoadingContainer>
  </div>
</template>

<style scoped>
#main {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
