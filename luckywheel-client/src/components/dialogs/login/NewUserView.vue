<script setup lang="ts">
import UserForm from './UserForm.vue';
import type {UserCredentials} from "@/models/user-credentials.ts";
import {useRouter} from "vue-router";
import UserAuthService from "@/services/user-auth.service.ts";
import {ref} from "vue";

const router = useRouter();

const hasError = ref(false);

function registerUser(creds: UserCredentials) {
  UserAuthService.registerUser(creds).then((success) => {
    if (success) {
        router.push("/");
    } else {
      hasError.value = true;
    }
  })
}
</script>
<template>
  <UserForm submit-button-text="Register"
            secondary-button-text="User already exists?"
            :show-criteria="true"
            :has-error="hasError"
            @submit="registerUser($event)"/>
</template>
<style scoped>
</style>
