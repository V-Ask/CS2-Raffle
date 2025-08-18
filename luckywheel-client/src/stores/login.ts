import { defineStore } from 'pinia'
import type {UserCredentials} from "@/models/user-credentials.ts";
import UserAuth from "@/services/user-auth.ts";

export const useLoginStore = defineStore('login-view', {
  state: () => ({
    loggingIn: true,
    passwordValid: true
  }),
  getters: {
    isLoggingIn: (state) => state.loggingIn,
    isPasswordValid: (state) => state.passwordValid
  },
  actions: {
    toggleLoggingIn() {
      this.loggingIn = !this.loggingIn
    },
    setPasswordValidity(isValid: boolean) {
      this.passwordValid = isValid
    }
  },
})
