<script setup lang="ts">
import DropdownSelector from "@/components/inputs/DropdownSelector.vue";
import {usePlaylistDropdownActions} from "@/composables/playlist-dropdown-actions.ts";

const {
  playlistIndices,
  selectedPlaylistIndex,
  playlistIndexOptionFn,
  playlistIndexTextFn
} = usePlaylistDropdownActions();
</script>

<template>
  <DropdownSelector v-model="selectedPlaylistIndex"
                    :options="playlistIndices"
                    :option-index-fn="playlistIndexOptionFn"
                    :option-text-fn="playlistIndexTextFn">
    SELECT PLAYLIST
  </DropdownSelector>
</template>

<style scoped>

</style>
