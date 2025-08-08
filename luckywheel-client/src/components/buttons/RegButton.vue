<script setup lang="ts">
const emit = defineEmits<{
  clicked: []
}>();
</script>
<template>
  <button class="button" @click="emit('clicked')">
    <slot></slot>
  </button>
</template>
<style scoped>
.button {
  font-size: large;
  padding: 12px 18px;
}
</style>
