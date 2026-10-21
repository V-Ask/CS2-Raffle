<script setup lang="ts">

import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import RegButton from "@/components/buttons/RegButton.vue";

const props = withDefaults(defineProps<{
  headerText: string,
  confirmButtonText: string,
  cancelButtonText: string,
}>(), {
  headerText: "Are you sure?",
  confirmButtonText: "Confirm",
  cancelButtonText: "Cancel",
});

const emits = defineEmits([
  'confirm',
  'cancel'
]);

</script>

<template>
  <StandardDialog :header-text="props.headerText">
    <template v-slot:content>
      <div class="buttons">
        <ConfirmButton @clicked="emits('confirm')">{{ props.confirmButtonText }}</ConfirmButton>
        <RegButton @clicked="emits('cancel')">{{ props.cancelButtonText }}</RegButton>
      </div>
    </template>
  </StandardDialog>
</template>

<style scoped>
.buttons {
  display: flex;
  justify-content: flex-end;
  gap: 1rem;
}
</style>
