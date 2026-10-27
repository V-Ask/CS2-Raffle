<script setup lang="ts">
import MapListPanel from "@/components/views/main-page/sub-panels/MapListPanel.vue";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";

const selectedPlaylistStore = useSelectedPlaylistStore();

const props = defineProps<{
  searchString: string;
}>();
</script>

<template>

  <template v-if="selectedPlaylistStore.selectedPlaylist">
    <p v-if="selectedPlaylistStore.isPlaylistEmpty"
       class="state-message">
      Add a map to this playlist to get started!
    </p>
    <p v-else-if="selectedPlaylistStore.isSortedPlaylistEmpty"
       class="state-message">
      Your current sorting options hide maps on this playlist
    </p>
    <MapListPanel v-else
                  :search-string="props.searchString"
    />
  </template>
  <p v-else class="state-message">Select a playlist to preview its maps.</p>
</template>

<style>
.state-message {
  color: #8a9bb0;
  text-align: center;
  padding-top: 3rem;
  letter-spacing: 0.05em;
}
</style>
