<script setup lang="ts">
import type {ReelMap} from "@/models/reel-map.ts";
import {ref} from "vue";

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

const incrementWeight = ref<boolean>(true);

function cancel() {
  emits('cancelWinningMap')
}

function playMap() {
  emits('weightChanged',)
}
</script>

<template>

</template>

<style scoped>

</style>
