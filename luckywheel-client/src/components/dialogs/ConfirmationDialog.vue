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
    <ConfirmButton>{{ props.confirmButtonText }}</ConfirmButton>
    <RegButton>{{ props.cancelButtonText }}</RegButton>
  </StandardDialog>
</template>

<style scoped>

</style>
