import { defineStore } from 'pinia'

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
