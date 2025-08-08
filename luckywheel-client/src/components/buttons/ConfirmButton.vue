<script setup lang="ts">
import RegButton from './RegButton.vue';

const emits = defineEmits<{
  clicked: []
}>();
</script>
<template>
  <RegButton @clicked="emits('clicked')">
    <slot></slot>
  </RegButton>
</template>
<style scoped>
button {
  background-color: green;
}
</style>
