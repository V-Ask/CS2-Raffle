<script setup lang="ts">
import SingleLineTextField from "@/components/inputs/textfield/SingleLineTextField.vue";
import ConfirmButton from "@/components/buttons/ConfirmButton.vue";
import {ref, watch} from "vue";
import {DathostGameHostService} from "@/services/game-host/dathost-game-host.service.ts";

const emits = defineEmits<{
  hostChanged: [DathostGameHostService]
}>();

const shouldSaveGameHostToStorage = defineModel<boolean>('shouldSaveGameHostToStorage');

const dathostService = ref(new DathostGameHostService());

const loadingVerification = ref(false);
const isVerified = ref(false);

function verifyDathost() {
  if(loadingVerification.value) {
    return;
  }
  loadingVerification.value = true;
  dathostService.value.testConnection().then(result => {
    isVerified.value = result;
    loadingVerification.value = false;
  });
}

watch(dathostService, (newValue) => {
  isVerified.value = false;
  emits("hostChanged", newValue);
}, { deep: true });
</script>

<template>
  <div class="input-wrapper">
    <div class="form-field">
      <label id="dathost-server-id">Server ID:</label>
      <SingleLineTextField v-model="dathostService.serverId" label-id="dathost-username" class="field"/>
    </div>
    <div class="form-field">
      <label id="dathost-username">Username:</label>
      <SingleLineTextField v-model="dathostService.username" label-id="dathost-username" class="field"/>
    </div>
    <div class="form-field">
      <label id="dathost-password">Password:</label>
      <SingleLineTextField v-model="dathostService.password" label-id="dathost-password" :is-password="true"
                           class="field"/>
    </div>
    <div class="test-button-wrapper">
      <ConfirmButton @clicked="verifyDathost()" :disabled="loadingVerification">Test</ConfirmButton>
      <div class="footer-side-wrapper">
        <div class="verification-wrapper">
          <label>Verified:</label>
          <i class="fa-solid fa-spinner fa-spin-pulse" v-if="loadingVerification"></i>
          <i class="verified fa-solid fa-check" v-else-if="isVerified"></i>
          <i class="not-verified fa-solid fa-x" v-else></i>
        </div>
        <div v-show="isVerified" class="remember-wrapper">
          <label>Remember?</label>
          <input type="checkbox" v-model="shouldSaveGameHostToStorage" :disabled="!isVerified"/>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
label {
  font-size: 1rem;
}

.input-wrapper {
  display: grid;
  gap: 1rem;
}

.form-field {
  display: grid;
  grid-auto-flow: row;
}

.field {
  font-size: 1rem;
}

.test-button-wrapper {
  display: flex;
  gap: 1rem;
}

.footer-side-wrapper {
  display: flex;
  flex-direction: column;
}

.verification-wrapper {
  display: flex;
  gap: 0.5rem;
}

p {
  margin: 0;
}

.verified {
  color: green;
}

.not-verified {
  color: red;
}
</style>
