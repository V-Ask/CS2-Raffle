<script setup lang="ts">
import {computed, onMounted, ref} from "vue";
import {useSpinnerStore} from "@/stores/spinner.store.ts";
import EditPlaylistsComponent
  from "@/components/views/edit-playlists/playlists/EditPlaylistsComponent.vue";

const spinnerStore = useSpinnerStore();
const isLoading = ref(true);

const playlistIndices = computed(() => spinnerStore.playlistIndices);

onMounted(async () => {
  await spinnerStore.ensurePlaylistIndices();
  isLoading.value = false;
})
</script>

<template>
  <div class="view-wrapper">
      <div v-if="isLoading"
           class="loading-wrapper">
        <i class="fa-solid fa-spinner fa-spin fa-4x"></i>
      </div>
    <div v-else class="playlists-wrapper">
      <EditPlaylistsComponent :playlists="playlistIndices" />
    </div>
  </div>
</template>

<style scoped>
.view-wrapper {
  width: 100%;
  height: 100%;
  display: grid;
  grid-template-rows: 1fr auto;
  background: var(--content-background)
}

.loading-wrapper {
  display: flex;
  height: 100%;
  justify-content: center;
  align-items: center;
}
</style>
