<script setup lang="ts">
import RegSubmitButton from "@/components/buttons/forms/RegSubmitButton.vue";

defineProps<{
  disabled?: boolean;
}>();

const emits = defineEmits<{
  clicked: [],
}>();
</script>

<template>
  <RegSubmitButton :disabled="disabled" @clicked="emits('clicked')">
    <slot></slot>
  </RegSubmitButton>
</template>

<style scoped>
button {
  background-color: green;
}
</style>
