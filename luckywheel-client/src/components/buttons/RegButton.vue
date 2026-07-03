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
  <button class="button hover-button"
          :type="props.type"
          :disabled="disabled"
          :class="{disabled: disabled}"
          @click="emit('clicked')">
    <slot></slot>
  </button>
</template>
<style scoped>
button {
  background-color: grey;
  font-size: large;
  padding: 12px 18px;
}

.disabled {
  opacity: 0.2;
  cursor: default;
}

:not(.disabled).hover-button:hover {
  border: 2px solid #1e1e1e;
}
</style>
