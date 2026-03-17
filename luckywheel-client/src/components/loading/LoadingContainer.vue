<script setup lang="ts">
import {useLoadingStore} from "@/stores/loading.store.ts";

const loadingStore = useLoadingStore();
</script>

<template>
  <div class="loading-overlay" v-if="loadingStore.isLoading" />
  <div class="loading-wrapper">
    <slot />
  </div>
</template>

<style scoped>
.loading-wrapper {
  height: 100%;
  width: 100%;
  display: flex;
}

.loading-overlay {
  position: absolute;
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.75);
  z-index: 999;
}
</style>
