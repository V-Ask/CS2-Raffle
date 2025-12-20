<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import {ref} from "vue";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";

const props = defineProps<{
  winningMap: ReelMap
}>();

const emits = defineEmits<{
  cancelWinningMap: [],
  weightChanged: [value: {
    previous: number,
    current: number
  }]
}>();

const gameHostStore = useGameHostStore();

const incrementWeight = ref<boolean>(true);

function cancel() {
  emits('cancelWinningMap');
}

function playMap() {
  if (incrementWeight.value) {
    const prevWeight = props.winningMap.weight;
    emits('weightChanged', {
      previous: prevWeight,
      current: prevWeight + 1
    });
  }

  gameHostStore.setWorkshopMap(props.winningMap);
}
</script>

<template>
  <div class="wrapper">
    <div class="content-wrapper">
      <div class="icon-wrapper">
        <MapIcon :map="props.winningMap"/>
      </div>
      <div class="button-row">
        <ConfirmButton @clicked="playMap()">Play</ConfirmButton>
        <RegButton @clicked="cancel()">Cancel</RegButton>
      </div>
    </div>
  </div>
</template>

<style scoped>
.wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
}

.content-wrapper {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.button-row {
  display: flex;
  gap: 1rem;
}
</style>
