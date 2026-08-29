<script setup lang="ts">
import UserForm from './UserForm.vue';
import type {UserCredentials} from "@/models/user-credentials.ts";
import UserAuthService from "@/services/user-auth.service.ts";
import {useRouter} from "vue-router";

const router = useRouter();

function login(creds: UserCredentials) {
  UserAuthService.loginUser(creds).then((success) => {
    if (success) {
      router.push("/");
    }
  })
}
</script>
<template>
  <UserForm submit-button-text="Login"
            secondary-button-text="Create new user?"
            @submit="login($event)"/>
</template>
