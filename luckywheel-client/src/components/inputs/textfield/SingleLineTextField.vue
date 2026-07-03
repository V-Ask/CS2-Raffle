<script setup lang="ts">
import {computed} from 'vue';
import {InputTypes} from "@/models/input-types.ts";
import type {SingleLineTextfieldProp} from "@/models/props/single-line-textfield.prop.ts";

const props = defineProps<SingleLineTextfieldProp>();
const model = defineModel<string>();

const inputType = computed(() => {
  switch (props.inputType) {
    case InputTypes.TEXT:
      return 'text';
    case InputTypes.SEARCH:
      return 'search';
    case InputTypes.PASSWORD:
      return 'password';
  }
})
</script>

<template>
  <input :type="inputType"
         :value="model"
         @input="model = ($event.target as HTMLInputElement).value"
         :maxlength="props.maxLength"
         :disabled="props.disabled"
         :placeholder="props.placeholder"
         :alt="props.alt"
         :aria-labelledby="props.labelId"
         :class="{disabled: props.disabled}"
  />
</template>

<style scoped>
.disabled {
  opacity: 0.4;
}
</style>
