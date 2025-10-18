import { defineStore } from 'pinia'
import type {User} from "@/models/user.ts";
import Auth from "@/api/auth.ts";

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as User | null,
  }),
  getters: {
    isLoggedIn: (state) => !!state?.user,
  },
});
