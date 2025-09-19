<script setup lang="ts">
import UserForm from './UserForm.vue';
import type {UserCredentials} from "@/models/user-credentials.ts";
import UserAuthService from "@/services/user-auth.service.ts";

function login(creds: UserCredentials) {
  UserAuthService.loginUser(creds);
}
</script>
<template>
  <UserForm submit-button-text="Login"
            secondary-button-text="Create new user?"
            @submit="login($event)"/>
</template>
