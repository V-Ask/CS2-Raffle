<script setup lang="ts">

import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import type {SingleLineTextfieldProp} from "@/models/props/single-line-textfield.prop.ts";

interface ReadonlySingleLineTextFieldProps extends SingleLineTextfieldProp {
  textFieldValue: string;
  showCopyToClipboardButton?: boolean
}

const props = defineProps<ReadonlySingleLineTextFieldProps>();

function copyToClipboard() {
  navigator.clipboard.writeText(props.textFieldValue);
}
</script>

<template>
  <div class="text-field-wrapper">
  <SingleLineTextField :input-type="props.inputType"
                       :alt="props.alt"
                       :placeholder="props.placeholder"
                       :label-id="props.labelId"
                       :disabled="true"
                       v-model="props.textFieldValue"
  />
  <button class="clipboard-button"
          @click="copyToClipboard"
  >
    <i class="fa-regular fa-clipboard fa-xl"
       title="Copy to clipboard"
    ></i>
  </button>
  </div>
</template>

<style scoped>
.text-field-wrapper {
  display: grid;
  grid-auto-flow: column;
  background-color: rgb(58, 58, 58);
}

.clipboard-button {
  margin-left: -32px;
  z-index: 1;
}

</style>
