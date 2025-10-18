<script setup lang="ts">
import type {Reel} from "@/models/reel.ts";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";
import {watch} from "vue";
import {useSpinnerStore} from "@/stores/spinner.store.ts";

const props = defineProps<{
  reel: Reel
}>();
const filledReel = props.reel.buildReel()


</script>

<template>
  <MapIcon :map="filledReel[69]"></MapIcon>
</template>

<style scoped>

</style>
