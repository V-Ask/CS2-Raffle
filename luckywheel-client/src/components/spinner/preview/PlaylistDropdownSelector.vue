<script setup lang="ts">
import DropdownSelector from "@/components/inputs/DropdownSelector.vue";
import {usePlaylistDropdownActions} from "@/composables/playlist-dropdown-actions.ts";
import {useSelectedPlaylistStore} from "@/stores/selected-playlist.store.ts";
import {usePlaylistStore} from "@/stores/spinner.store.ts";

const {
  selectedPlaylistIndex,
  playlistIndexOptionFn,
  playlistIndexTextFn
} = usePlaylistDropdownActions();

const playlistStore = usePlaylistStore();
</script>

<template>
  <DropdownSelector v-model="selectedPlaylistIndex"
                    :options="playlistStore.playlistIndices"
                    :option-index-fn="playlistIndexOptionFn"
                    :option-text-fn="playlistIndexTextFn">
    SELECT PLAYLIST
  </DropdownSelector>
</template>

<style scoped>

</style>
