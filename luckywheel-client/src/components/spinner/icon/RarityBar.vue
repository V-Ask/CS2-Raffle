<script setup lang="ts">
const props = defineProps<{
  rarity: number,
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
