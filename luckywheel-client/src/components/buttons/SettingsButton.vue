<script setup lang="ts">

import SettingsDialog from "@/components/dialogs/SettingsDialog.vue";
import {ref} from "vue";
import DialogService from "@/services/dialog.service.ts";

const settingsDialog = ref<HTMLDialogElement | null>(null);

function openSettings() {
  settingsDialog.value?.showModal();
}

function closeSettings() {
  settingsDialog.value?.close();
}

function handleBackdropClick(event: MouseEvent) {
  DialogService.handleBackdropClick(settingsDialog.value!, event, () => closeSettings());
}
</script>

<template>
  <button title="Settings" class="button-wrapper" @click="openSettings">
    <i title="Settings" class="outline fa-solid fa-gear fa-3x cog-icon"></i>
  </button>
  <dialog ref="settingsDialog" @click="handleBackdropClick">
    <SettingsDialog @closeDialog="closeSettings()"/>
  </dialog>
</template>

<style scoped>
.button-wrapper {
  color: initial;
}

.button-wrapper:hover .cog-icon {
  animation: rotateShake 0.4s ease-in-out;
}

.cog-icon {
  transition: transform 0.3s ease;
}

@keyframes rotateShake {
  0% {
    transform: rotate(0deg);
  }
  10% {
    transform: rotate(20deg) translateX(1px);
  }
  20% {
    transform: rotate(40deg) translateX(-1px);
  }
  30% {
    transform: rotate(60deg) translateX(1px);
  }
  40% {
    transform: rotate(80deg) translateX(-1px);
  }
  50% {
    transform: rotate(100deg) translateX(1px);
  }
  60% {
    transform: rotate(120deg) translateX(-1px);
  }
  70% {
    transform: rotate(140deg) translateX(1px);
  }
  80% {
    transform: rotate(160deg) translateX(-1px);
  }
  90% {
    transform: rotate(180deg) translateX(1px);
  }
  100% {
    transform: rotate(180deg);
  }
}
</style>
