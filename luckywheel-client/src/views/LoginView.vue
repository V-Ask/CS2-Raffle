<script setup lang="ts">
import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import NewUserView from "@/components/dialogs/login/NewUserView.vue";
import ExistingUserView from "@/components/dialogs/login/ExistingUserView.vue";
import {useLoginStore} from "@/stores/login.store.ts";

const loginStore = useLoginStore();

function getHeaderText() {
  return loginStore.isLoggingIn ? "Login" : "Register new user";
}
</script>
<template>
  <div class="login-wrapper">
    <div class="login-grid">
      <StandardDialog :header-text="getHeaderText()">
        <div class="flex-column">
          <NewUserView v-if="!loginStore.isLoggingIn"/>
          <ExistingUserView v-else/>
        </div>
      </StandardDialog>
    </div>
  </div>
</template>
<style>
.login-wrapper {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
.login-grid {
  height: 100%;
  width: 80%;
  max-width: 600px;
  display: grid;
  align-items: center;
  justify-content: center;
  grid-template-columns: 1fr;
}
</style>
