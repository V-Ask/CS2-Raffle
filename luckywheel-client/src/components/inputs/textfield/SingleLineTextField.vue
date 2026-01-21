<script setup lang="ts">

interface Props {
  maxLength?: number;
  disabled?: boolean;
  alt?: string;
  placeholder?: string;
  labelId?: string;
  isPassword?: boolean;
}

const props = defineProps<Props>();
const model = defineModel<string>();

function getInputType(): "password" | "text" {
  return props.isPassword ? "password" : "text";
}

</script>

<template>
  <input :type="getInputType()"
         :value="model"
         @input="model = ($event.target as HTMLInputElement).value"
         :maxlength="props.maxLength"
         :disabled="props.disabled"
         :placeholder="props.placeholder"
         :alt="props.alt"
         :aria-labelledby="props.labelId"
  />
</template>

<style scoped>

</style>
