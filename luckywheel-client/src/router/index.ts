import SpinView from '@/views/SpinView.vue'
import {
  createRouter,
  createWebHistory,
  type NavigationGuardNext,
} from 'vue-router'
import authGuard from "@/guards/auth-guard.ts";
import PlaylistSelectionView from "@/views/PlaylistSelectionView.vue";
import LoginView from "@/views/LoginView.vue";
import ConfirmEmailView from "@/views/ConfirmEmailView.vue";

const routes = [
  {
    path: '/',
    name: 'Main',
    component: PlaylistSelectionView,
  },
  {
    path: '/new-user',
    name: 'Login',
    component: LoginView,
  },
  {
    path: '/confirm-email',
    name: 'ConfirmEmail',
    component: ConfirmEmailView,
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

function redirectIfFail(predicate: (() => boolean), next: NavigationGuardNext, alternative: any) {
  if(predicate()) {
    next();
  } else {
    next(alternative);
  }
}

router.beforeEach(async (to, from, next) => {
  switch (to.name) {
    case 'Login':
      const success = await authGuard(from, to);
      redirectIfFail(() => !success, next, {
        name: 'Main',
        replace: true,
      })
      break;
    default:
      next();
  }
})

export default router
