<script setup lang="ts">
import {useDialogStore} from "@/stores/dialog.ts";
import LoginDialog from "@/components/dialogs/login/LoginDialog.vue";

const dialogStore = useDialogStore();
dialogStore.showDialog("Test Header", LoginDialog);
</script>

<template>
  <div id="main">
    <RouterView />
  </div>
</template>

<style scoped>
#main {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
