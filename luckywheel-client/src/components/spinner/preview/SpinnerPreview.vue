<script setup lang="ts">
import MapList from "@/components/spinner/preview/MapList.vue";
import SpinnerControls from "@/components/spinner/preview/SpinnerControls.vue";
import type {ReelMap} from "@/models/reel-map.ts";
import {useSpinnerStore} from "@/stores/spinner.ts";
import ReelService from "@/services/spinner/reel.service.ts";
import {useRouter} from "vue-router";

const props = defineProps<{
  coloredMaps: ReelMap[];
}>();
const spinnerStore = useSpinnerStore();
const router = useRouter();

function spinMap() {
  spinnerStore.spinning = true;
}

function closePlaylist() {
  return ReelService.navigateToPlaylistSelector(router);
}

</script>
<template>
  <div class="playlist wrapper">
    <MapList :workshop-maps="props.coloredMaps"></MapList>
    <SpinnerControls @spin="spinMap()"
                     @close="closePlaylist()" />
  </div>
</template>
