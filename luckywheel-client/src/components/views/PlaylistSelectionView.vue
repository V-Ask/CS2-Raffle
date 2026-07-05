<script setup lang="ts">
import PlaylistSelection from "@/components/inputs/PlaylistSelection.vue";
import CreatePlaylistButton from "@/components/create-playlist/CreatePlaylistButton.vue";

</script>

<template>
  <div class="input-wrapper">
    <PlaylistSelection/>
    <CreatePlaylistButton/>
  </div>
</template>

<style scoped>
.input-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 1rem;
}
</style>
