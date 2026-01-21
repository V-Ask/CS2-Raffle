export default {
  handleBackdropClick(dialog: HTMLDialogElement, event: MouseEvent, callback: ()  => void) {
  if (event.target === dialog) {
    callback();
  }
}
}
