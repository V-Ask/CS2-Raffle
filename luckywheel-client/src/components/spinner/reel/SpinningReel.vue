<script setup lang="ts">
import {useSpinnerStore} from "@/stores/spinner.ts";
import {ref, watch} from "vue";
import {WorkshopMap} from "@/models/workshop-map.ts";
import type {Reel} from "@/models/reel.ts";
import MapIcon from "@/components/spinner/icon/MapIcon.vue";

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
