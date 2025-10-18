<script setup lang="ts">
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinnerControls from "@/components/spinner/preview/SpinnerControls.vue";
import type {ReelMap} from "@/models/reel-map.ts";


const props = defineProps<{
  coloredMaps: ReelMap[];
}>();
</script>
<template>
  <div class="playlist wrapper">
    <MapList :workshop-maps="props.coloredMaps"></MapList>
    <SpinnerControls />
  </div>
</template>
