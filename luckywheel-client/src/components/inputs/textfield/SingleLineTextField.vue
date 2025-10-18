<script setup lang="ts">

interface Props {
  maxLength?: number;
  disabled?: boolean;
  alt?: string;
  placeholder?: string;
}

const props = defineProps<Props>();
const model = defineModel<string>();

</script>

<template>
  <input type="text"
         :value="model"
         @input="model = ($event.target as HTMLInputElement).value"
         :maxlength="props.maxLength"
         :disabled="props.disabled"
         :placeholder="props.placeholder"
         :alt="props.alt"
  />
</template>

<style scoped>

</style>
