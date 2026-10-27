<script setup lang="ts">

import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import CreatePlaylistForm from "@/components/create-playlist/CreatePlaylistForm.vue";
import type {WorkshopPlaylistView} from "@/models/workshop-playlist-view.ts";

const emits = defineEmits<{
  dialogClosed: [value: WorkshopPlaylistView | undefined]
}>();

function onFormClosed(playlistView?: WorkshopPlaylistView) {
  emits('dialogClosed', playlistView);
}
</script>

<template>
  <StandardDialog header-text="Create Playlist">
    <template v-slot:content>
      <CreatePlaylistForm @playlistCallback="onFormClosed($event)"/>
    </template>
  </StandardDialog>
</template>

<style scoped>

</style>
