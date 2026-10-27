import {type Ref, ref, watch} from "vue";
import DialogService from "@/services/dialog.service.ts";

export function useDialogActions(dialog: Ref<HTMLDialogElement | null>) {
  const isDialogOpen = ref(false);

  // The dialog can also be closed natively (e.g. by pressing Escape)
  watch(dialog, (newDialog, oldDialog) => {
    oldDialog?.removeEventListener('close', onNativeClose);
    newDialog?.addEventListener('close', onNativeClose);
  });

  function onNativeClose() {
    isDialogOpen.value = false;
  }

  function openDialog() {
    isDialogOpen.value = true;
    dialog.value?.showModal();
  }

  function closeDialog() {
    isDialogOpen.value = false;
    dialog.value?.close();
  }

  function handleBackdropClick(event: MouseEvent) {
    if (!dialog.value) return;
    DialogService.handleBackdropClick(dialog.value, event, closeDialog);
  }

  return {
    openDialog,
    closeDialog,
    handleBackdropClick,
    isDialogOpen
  }
}
