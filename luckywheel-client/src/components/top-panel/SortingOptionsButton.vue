<script setup lang="ts">
import {useDialogActions} from "@/composables/dialog-actions.ts";
import {ref} from "vue";
import SortingDialog from "@/components/top-panel/SortingDialog.vue";

const sortDialog = ref<HTMLDialogElement | null>(null);

const { openDialog, closeDialog, handleBackdropClick } = useDialogActions(sortDialog);
</script>

<template>
  <button @click="openDialog()"
          title="Sorting Options"
  >
    <i class="fa-solid fa-sort fa-2xl"></i>
  </button>
  <dialog ref="sortDialog" @click="handleBackdropClick">
    <SortingDialog @close="closeDialog()" />
  </dialog>
</template>

<style scoped>

</style>
