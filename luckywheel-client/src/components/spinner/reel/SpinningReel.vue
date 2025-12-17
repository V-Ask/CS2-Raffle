<script setup lang="ts">
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import {ref, onMounted} from "vue";
import type {ReelMap} from "@/models/reel-map.ts";
import ReelService from "@/services/spinner/reel.service.ts";

const props = defineProps<{
  reelSize: number,
  reelMaps: Set<ReelMap>,
}>();

const emits = defineEmits<{
  mapSelected: [value: ReelMap]
}>();

const filledReel = ReelService.buildRandomReel(props.reelSize, props.reelMaps);
const winningIndex = calcWinningMapIndex(props.reelSize);
const winningMap = filledReel[winningIndex];

const reelContainer = ref<HTMLElement | null>(null);

onMounted(() => {
  spinReel(props.reelSize).then(() => {
    emits('mapSelected', winningMap);
  })
});

function calcWinningMapIndex(reelLength: number) {
  let winningIndex = reelLength - 10; //some number that ensures that the end of the reel is not shown
  // If the reel size is larger than 10, let's select the third quadrant of the set
  if (reelLength <= winningIndex) {
    return Math.floor(0.75 * reelLength)
  }
  return winningIndex;
}

async function spinReel(winningIndex: number) {
  return Promise.resolve();
}

</script>

<template>
  <div class="reel-container" ref="reelContainer">
    <div class="reel">
      <div v-for="map in filledReel" class="icon">
        <MapIcon :map="map"></MapIcon>
      </div>
    </div>
  </div>
</template>

<style scoped>
.reel-container {
  width: 100%;
  max-width: 100vw;
  overflow-x: auto;
  overflow-y: hidden;
  pointer-events: none; /* Disable manual scrolling */
  -ms-overflow-style: none; /* Hide scrollbar for IE and Edge */
  scrollbar-width: none; /* Hide scrollbar for Firefox */
  user-select: none; /* Prevent text selection */
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
}

/* Hide scrollbar for Chrome, Safari and Opera */
.reel-container::-webkit-scrollbar {
  display: none;
}

.reel {
  display: flex;
  gap: 1rem;
  width: max-content; /* Ensure the reel takes full width of its content */
}

.icon {
  width: 20rem;
  height: 11.33rem;
}
</style>
