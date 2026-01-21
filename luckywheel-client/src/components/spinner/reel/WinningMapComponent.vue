<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import {computed, ref} from "vue";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";
import type {WinningMapActionCallback} from "@/models/winning-map-action-callback.ts";

const props = defineProps<{
  winningMap: ReelMap
}>();

const emits = defineEmits<{
  cancelWinningMap: [WinningMapActionCallback]
}>();

const gameHostStore = useGameHostStore();

const incrementWeight = ref<boolean>(true);
const removeMap =  ref<boolean>(false);
const computedCallback = computed(() => {
  return {
    shiftOtherWeights: incrementWeight.value,
    removeMap: removeMap.value
  }
})

function cancel() {
  emits('cancelWinningMap', emptyCallback());
}

function playMap() {
  emits('cancelWinningMap', computedCallback.value);
  gameHostStore.setWorkshopMap(props.winningMap);
}

function emptyCallback(): WinningMapActionCallback {
  return {
    shiftOtherWeights: false,
    removeMap: false,
  }
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
        <div class="checkbox-col">
          <label>
            Increment other map's weights?
            <input type="checkbox" v-model="incrementWeight"/>
          </label>
          <label>
            Remove map from playlist?
            <input type="checkbox" v-model="removeMap"/>
          </label>
        </div>
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

.checkbox-col {
  display: flex;
  flex-direction: column;
  font-size: 0.8rem;
}
</style>
