import { defineStore } from 'pinia'

export const useLoginStore = defineStore('login-view', {
  state: () => ({
    loggingIn: true,
  }),
  getters: {
    isLoggingIn: (state) => state.loggingIn,
  },
  actions: {
    toggleLoggingIn() {
      console.log('123123123')
      this.loggingIn = !this.loggingIn
    },
  },
})
