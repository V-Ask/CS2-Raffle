<script setup lang="ts">
import { useLoginStore } from '@/stores/login';
import StandardDialog from "@/components/dialogs/StandardDialog.vue";
import NewUserView from "@/components/dialogs/login/NewUserView.vue";
import ExistingUserView from "@/components/dialogs/login/ExistingUserView.vue";

const loginStore = useLoginStore();
</script>
<template>
  <StandardDialog header-text="Login">
    <div class="flex-column">
      <NewUserView v-if="!loginStore.isLoggingIn" />
      <ExistingUserView v-else />
    </div>
  </StandardDialog>
</template>
