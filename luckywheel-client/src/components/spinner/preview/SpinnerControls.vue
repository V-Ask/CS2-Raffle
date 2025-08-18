<script setup lang="ts">
const emits = defineEmits<{
  (e: 'close'): void,
  (e: 'spin'): void,
}>();
</script>

<template>
  <button @click="emits('spin')">RANDOMLY SELECT MAP</button>
  <button @click="emits('close')">CLOSE</button>
</template>

<style scoped>

</style>
