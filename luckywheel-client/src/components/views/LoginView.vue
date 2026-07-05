<script setup lang="ts">
import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import UserForm from "@/components/dialogs/login/UserForm.vue";
import {useLoginStore} from "@/stores/login.store.ts";
import { onMounted, ref, computed } from "vue";
import { onUnmounted } from "vue";
import type {UserCredentials} from "@/models/user-credentials.ts";
import UserAuthService from "@/services/user-auth.service.ts";
import {useRouter} from "vue-router";
import {useLoadingStore} from "@/stores/loading.store.ts";

const loadingStore = useLoadingStore();
const loginStore = useLoginStore();
const router = useRouter();

const newUserDialog = ref<HTMLDialogElement | null>(null);

onMounted(() => {
  newUserDialog.value?.showModal();
});

onUnmounted(() => {
  newUserDialog.value?.close();
});

const headerText = computed(() => loginStore.isLoggingIn ? "Login" : "Register new user");
const submitText = computed(() => loginStore.isLoggingIn ? "Login" : "Register");
const secondaryText = computed(() => loginStore.isLoggingIn ? "Create new user?" : "User already exists?");

function handleSubmit(creds: UserCredentials) {
  const stopLoadingCallback = loadingStore.startLoading();
  if (loginStore.isLoggingIn) {
    UserAuthService.loginUser(creds).then(success => {
      if (success) router.push("/")
      stopLoadingCallback();
    });
  } else {
    UserAuthService.registerUser(creds).then(success => {
      if (success) router.push("/")
      stopLoadingCallback();
    });
  }
}
</script>
<template>
  <div class="login-wrapper">
    <div class="login-grid">
      <dialog ref="newUserDialog">
        <StandardDialog :header-text="headerText">
          <UserForm
            :submit-button-text="submitText"
            :secondary-button-text="secondaryText"
            :show-criteria="!loginStore.isLoggingIn"
            @submit="handleSubmit($event)"
          />
        </StandardDialog>
      </dialog>
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
