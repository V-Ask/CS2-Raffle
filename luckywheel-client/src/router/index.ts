import SpinView from '@/views/SpinView.vue'
import { createRouter, createWebHistory } from 'vue-router'
import LoginDialog from "@/components/dialogs/login/LoginDialog.vue";

const routes = [
  {
    path: '/',
    name: 'Main',
    component: SpinView,
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginDialog
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

//router.beforeEach(async (to) => {
//  const authStore = useAuthStore()
//  if (to.name !== 'Login' && !authStore.isLoggedIn) {
//    return { name: 'Login' }
//  }
//})

export default router
