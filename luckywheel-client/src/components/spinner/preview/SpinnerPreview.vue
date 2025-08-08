<script setup lang="ts">
import {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import PreviewHeader from "@/components/spinner/preview/PreviewHeader.vue";
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinnerControls from "@/components/spinner/preview/SpinnerControls.vue";

const props = defineProps<{
  selectedPlaylist: WorkshopPlaylist
}>();
</script>
<template>
  <div class="wrapper flex-column align-center">
    <div>
      <PreviewHeader :playlist-name="props.selectedPlaylist.playlistName" />
    </div>
    <div class="bottom-align flex-column align-center">
      <div>
        <MapList :workshop-maps="props.selectedPlaylist.workshopMaps" />
      </div>
      <div>
        <SpinnerControls />
      </div>
    </div>
  </div>
</template>
<style scoped>
.wrapper {
  height: 100%;
}
.bottom-align {
  flex-grow: 1;
  justify-content: flex-end;
}
</style>
