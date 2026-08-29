<script setup lang="ts">
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import { ref, onMounted } from "vue";
import type { ReelMap } from "@/models/reel-map.ts";
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
const translateX = ref(0);
const settled = ref(false);

const SPIN_DURATION = 8000;

function calcWinningMapIndex(reelLength: number): number {
  const idx = reelLength - 10;
  return reelLength <= idx ? Math.floor(0.75 * reelLength) : idx;
}

onMounted(() => {
  if (!reelContainer.value) return;

  const remPx = parseFloat(getComputedStyle(document.documentElement).fontSize);
  const slotPx = 21 * remPx;      // 20rem width + 1rem gap
  const itemHalfPx = 10 * remPx;  // half of 20rem
  const containerWidth = reelContainer.value.clientWidth;
  const finalOffset = -(winningIndex * slotPx + itemHalfPx - containerWidth / 2);

  requestAnimationFrame(() => {
    requestAnimationFrame(() => {
      translateX.value = finalOffset;
    });
  });

  setTimeout(() => {
    settled.value = true;
    emits('mapSelected', winningMap);
  }, SPIN_DURATION + 500);
});
</script>

<template>
  <div class="reel-container" ref="reelContainer">
    <div class="center-marker" />
    <div class="reel" :style="{ transform: `translateX(${translateX}px)` }">
      <div v-for="(map, i) in filledReel"
           :key="i"
           class="icon"
           :class="{ winner: settled && i === winningIndex }">
        <MapIcon :map="map" />
      </div>
    </div>
  </div>
</template>

<style scoped>
.reel-container {
  position: relative;
  width: 100%;
  height: 100%;
  overflow: hidden;
  display: flex;
  align-items: center;
  pointer-events: none;
  user-select: none;
  -webkit-user-select: none;
}

.reel-container::before,
.reel-container::after {
  content: '';
  position: absolute;
  top: 0;
  bottom: 0;
  width: 25%;
  z-index: 1;
  pointer-events: none;
}

.reel-container::before {
  left: 0;
  background: linear-gradient(to right, var(--content-background) 0%, transparent 100%);
}

.reel-container::after {
  right: 0;
  background: linear-gradient(to left, var(--content-background) 0%, transparent 100%);
}

.center-marker {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 50%;
  width: 2px;
  background: red;
  transform: translateX(-50%);
  z-index: 2;
}

.center-marker::before,
.center-marker::after {
  content: '';
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  width: 0;
  height: 0;
}

.center-marker::before {
  top: 0;
  border-left: 9px solid transparent;
  border-right: 9px solid transparent;
  border-top: 16px solid red;
}

.reel {
  display: flex;
  gap: 1rem;
  width: max-content;
  transition: transform 8s cubic-bezier(0.16, 1, 0.3, 1);
  will-change: transform;
}

.icon {
  width: 20rem;
  height: 11.33rem;
  flex-shrink: 0;
  transition: box-shadow 0.4s ease;
}

.icon.winner {
  box-shadow: 0 0 24px 8px rgba(240, 192, 64, 0.6);
}
</style>
