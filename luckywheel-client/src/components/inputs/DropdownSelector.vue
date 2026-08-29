<script setup lang="ts" generic="T">
const props = defineProps<{
  options: T[]
  optionIndexFn: (option: T) => string,
  optionTextFn: (option: T) => string,
  disabled?: boolean,
}>();

const model = defineModel<T | null>({ default: null });
</script>

<template>
  <select class="selector" :disabled="disabled" v-model="model">
    <option class="default-option"
            :value="null"
            disabled>
      <slot/>
    </option>
    <option v-for="option in props.options"
            :key="props.optionIndexFn(option)"
            :value="option">
      {{ props.optionTextFn(option) }}
    </option>
  </select>
</template>

<style scoped>
.selector {
  appearance: none;
  -webkit-appearance: none;
  width: 360px;
  padding: 0 2.5rem 0 0.75rem;
  background-color: #1e2a35;
  border: 1px solid #3a5068;
  color: #c6d4df;
  font-family: inherit;
  font-size: 0.9rem;
  letter-spacing: 0.03em;
  cursor: pointer;
  outline: none;
  transition: border-color 0.15s, background-color 0.15s;
}

.selector:hover {
  background-color: #253342;
  border-color: #5ba3d9;
}

.selector:focus {
  border-color: #5ba3d9;
  box-shadow: 0 0 0 2px rgba(91, 163, 217, 0.15);
}

.selector option {
  background-color: #131e2a;
  color: #c6d4df;
}

.select-chevron {
  position: absolute;
  right: 0.65rem;
  width: 12px;
  height: 12px;
  color: #5ba3d9;
  pointer-events: none;
}
</style>
