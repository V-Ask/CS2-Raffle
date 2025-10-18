<script setup lang="ts">
const props = defineProps<{
  type?: "button" | "submit" | "reset" | undefined,
  disabled?: boolean | undefined,
}>();

const emit = defineEmits<{
  clicked: []
}>();
</script>
<template>
  <button class="button" :type="props.type" :disabled="disabled" @click="emit('clicked')">
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
