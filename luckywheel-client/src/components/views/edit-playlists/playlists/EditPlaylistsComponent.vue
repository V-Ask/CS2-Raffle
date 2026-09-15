<script setup lang="ts">
import type {WorkshopPlaylistIndex} from "@/api/dto/indices/workshop-playlist-index.ts";
import EditPlaylistComponent
  from "@/components/views/edit-playlistIndices/playlistIndices/playlist/EditPlaylistComponent.vue";
import {computed} from "vue";

const props = defineProps<{
  playlistIndices: WorkshopPlaylistIndex[]
}>();

const playlistsEmpty = computed(() => props.playlistIndices.length === 0);

</script>

<template>
  <div v-if="playlistsEmpty">
    No playlistIndices exist. Click here to create one!
  </div>
  <template v-else v-for="playlist in props.playlistIndices">
    <EditPlaylistComponent :playlist="playlist" />
  </template>
</template>

<style scoped>

</style>
