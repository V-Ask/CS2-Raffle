<script setup lang="ts">
import type {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import EditPlaylistComponent
  from "@/components/views/edit-playlists/playlists/playlist/EditPlaylistComponent.vue";
import {computed} from "vue";

const props = defineProps<{
  playlists: WorkshopPlaylistIndex[]
}>();

const playlistsEmpty = computed(() => props.playlists.length === 0);

</script>

<template>
  <div v-if="playlistsEmpty">
    No playlists exist. Click here to create one!
  </div>
  <template v-else v-for="playlist in props.playlists">
    <EditPlaylistComponent :playlist="playlist" />
  </template>
</template>

<style scoped>

</style>
