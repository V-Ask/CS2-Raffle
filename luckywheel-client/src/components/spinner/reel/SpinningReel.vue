<script setup lang="ts">
import type {Reel} from "@/models/reel.ts";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import {watch, ref, onMounted} from "vue";
import type {ReelMap} from "@/models/reel-map.ts";
import {useSpinnerStore} from "@/stores/spinner.store.ts";

const props = defineProps<{
  reel: Reel,
  reelSize: number,
}>();

const spinnerStore = useSpinnerStore();

const filledReel = props.reel.buildRandomReel(props.reelSize);
const winningIndex = calcWinningMapIndex(filledReel, props.reelSize);
const winningMap = filledReel[winningIndex];

const reelContainer = ref<HTMLElement | null>(null);

onMounted(() => {
  spinReel(props.reelSize).then(() => {
    spinnerStore.selectMap()
  })
});

function calcWinningMapIndex(filledReel: ReelMap[], reelLength: number) {
  let winningIndex = reelLength - 10; //some number that ensures that the end of the reel is not shown
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
