<script setup lang="ts">

import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import {SpinnerStatus, useSpinnerStore} from "@/stores/spinner.store.ts";
import RegButton from "@/components/buttons/RegButton.vue";
import RoutingService from "@/services/routing.service.ts";

const emits = defineEmits(['spinReel', 'cancelSpin', 'addMap'])
</script>

<template>
  <div class="control-group">
    <ConfirmButton class="button" @click="emits('spinReel')">Spin</ConfirmButton>
    <RegButton class="button" @click="emits('cancelSpin')">Close</RegButton>
    <ConfirmButton class="button" @click="emits('addMap')">Add Map</ConfirmButton>
  </div>
</template>

<style scoped>
.control-group {
  width: 100%;
  display: flex;
  gap: 0.5rem;
  justify-content: flex-end;
  padding: 1rem;
  border: 4px solid black;

  .button {
    height: 3.5rem;
  }
}
</style>
