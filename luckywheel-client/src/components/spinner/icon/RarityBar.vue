<script setup lang="ts">
import type {Color} from "@/types/color.ts";

const props = defineProps<{
  rarityColor: Color,
}>();
</script>

<template>
  <div class="bar"></div>
</template>

<style scoped>
.bar {
  background-color: red;
  width: 2px;
  height: 100%;
}
</style>
