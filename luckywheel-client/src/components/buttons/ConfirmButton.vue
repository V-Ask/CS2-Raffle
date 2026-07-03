<script setup lang="ts">
import RegButton from './RegButton.vue';

const props = defineProps<{
  type?: "button" | "submit" | "reset" | undefined,
  disabled?: boolean | undefined,
}>();

const emits = defineEmits(['clicked'])
</script>
<template>
  <RegButton
    :type="props.type"
    :disabled="disabled"
    @clicked="emits('clicked')">
    <slot></slot>
  </RegButton>
</template>
<style scoped>
button {
  background-color: green;
}
</style>
