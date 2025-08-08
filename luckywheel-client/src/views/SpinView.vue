<script setup lang="ts">
import {WorkshopPlaylist} from "@/models/workshop-playlist.ts";
import {WorkshopMap} from "@/models/workshop-map.ts";
import SpinnerPreview from "@/components/spinner/preview/SpinnerPreview.vue";

const testPlaylist = new WorkshopPlaylist("Test Playlist", [
  new WorkshopMap('Test map', 'test id', 1, new ArrayBuffer)
])
</script>
<template>
  <SpinnerPreview :selected-playlist="testPlaylist" />
</template>
