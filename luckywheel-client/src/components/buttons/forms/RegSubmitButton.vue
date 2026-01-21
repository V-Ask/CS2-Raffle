<script setup lang="ts">
defineProps<{
  disabled?: boolean;
}>();

const emits = defineEmits<{
  clicked: [],
}>();
</script>

<template>
  <button class="hover-button" type="submit" :disabled="disabled" @click="emits('clicked')">
    <slot></slot>
  </button>
</template>

<style scoped>
button {
  background-color: grey;
  font-size: large;
  padding: 12px 18px;
}
</style>
