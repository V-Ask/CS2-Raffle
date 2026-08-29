<script setup lang="ts">
const props = defineProps<{
  playlistName: string;
}>();
</script>

<template>
  <div class="header-box">
    <h1>Spin Wheel</h1>
    <h2>{{ props.playlistName }}</h2>
  </div>
</template>

<style scoped>
.header-box {
  text-align: center;
}
</style>
