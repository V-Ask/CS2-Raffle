<script setup lang="ts">

import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import {InputTypes} from "@/models/input-types.ts";
import ConfirmSubmitButton from "@/components/buttons/forms/ConfirmSubmitButton.vue";
import {useGameHostStore} from "@/stores/game-host.store.ts";

const emits = defineEmits<{
  passwordSet: [value: string],
}>();

const gameStore = useGameHostStore();

function confirmPassword() {
  emits('passwordSet', gameStore.savedPassword);
}


</script>

<template>
  <StandardDialog header-text="Enter password...">
    <div class="form-wrapper">
      <SingleLineTextField :input-type="InputTypes.PASSWORD" v-model="gameStore.savedPassword"/>
      <ConfirmSubmitButton @click="confirmPassword">Confirm</ConfirmSubmitButton>
    </div>
    <p class="flavor">This is saved until you refresh.</p>
  </StandardDialog>
</template>

<style scoped>
.form-wrapper {
  display: grid;
  grid-auto-flow: column;
  grid-template-columns: 1fr auto;
  gap: 1rem;
}
</style>
