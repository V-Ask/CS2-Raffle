import { loginPost } from '@/api/auth'
import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null,
  }),
  getters: {
    isLoggedIn: (state) => !!state?.user,
  },
  actions: {
    async login(username: string, password: string) {
      loginPost(username, password)
        .then((response) => {
          console.log(response)
        })
        .catch((e) => {
          console.error(e)
        })
    },
  },
})
