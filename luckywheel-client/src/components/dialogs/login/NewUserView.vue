<script setup lang="ts">
import UserForm from './UserForm.vue';
import type {UserCredentials} from "@/models/user-credentials.ts";
import userAuth from "@/services/user-auth.ts";
import {useRouter} from "vue-router";

const router = useRouter();

function registerUser(creds: UserCredentials) {
  userAuth.registerUser(creds).then((success) => {
    if (success) {
        router.push("/");
    }
  })
}
</script>
<template>
  <UserForm submit-button-text="Register"
            secondary-button-text="User already exists?"
            :show-criteria="true"
            @submit="registerUser($event)"/>
</template>
<style scoped>
</style>
