import {createWebHistory,} from 'vue-router'
import {LOGIN_NAME, NOT_FOUND_NAME, PLAYLIST_VIEW} from "@/helpers/constants/routing.ts";
import {GuardedRouter} from "@/router/guarded-router.ts";
import {authGuardFn} from "@/guards/auth-guard.ts";
import {useLoadingStore} from "@/stores/loading.store.ts";
import LoginView from "@/components/views/LoginView.vue";
import MainPageView from "@/components/views/main-page/MainPageView.vue";
import NotFoundView from "@/components/views/NotFoundView.vue";

const routes = [
  {
    path: '/new-user',
    name: LOGIN_NAME,
    component: LoginView,
  },
  {
    path: '/',
    name: PLAYLIST_VIEW,
    component: MainPageView,
  },
  // {
  //   path: '/playlists',
  //   name: EDIT_PLAYLIST_VIEW,
  //   component: EditPlaylistsView
  // },
  {
    path: '/:path',
    name: NOT_FOUND_NAME,
    component: NotFoundView
  }
]

const API: GuardedRouter = new GuardedRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

// Redirects to the login page if user is unauthorized
API.guardAllRoutes(authGuardFn, [LOGIN_NAME], {
  name: LOGIN_NAME,
});

function setupLazyLoadingStore() {
  API.router.beforeEach((to, from, next) => {
    const loadingStore = useLoadingStore();
    loadingStore.startLazyLoading();
    next();
  });

  API.router.afterEach((to, from, next) => {
    const loadingStore = useLoadingStore();
    loadingStore.stopLazyLoading();
  })
}

setupLazyLoadingStore();


export default API
