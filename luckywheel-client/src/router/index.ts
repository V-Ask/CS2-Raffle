import {
  createWebHistory,
} from 'vue-router'
import LoginView from "@/views/LoginView.vue";
import {
  CREATE_NEW_PLAYLIST_NAME,
  LOGIN_NAME, NOT_FOUND_NAME, PLAYLIST_SELECTED_NAME, PLAYLIST_VIEW
} from "@/helpers/constants/routing.ts";
import PlaylistView from "@/views/PlaylistView.vue";
import PlaylistSelectionView from "@/views/PlaylistSelectionView.vue";
import {GuardedRouter} from "@/router/guarded-router.ts";
import {authGuardFn} from "@/guards/auth-guard.ts";
import NotFoundView from "@/views/NotFoundView.vue";
import CreatePlaylistView from "@/views/CreatePlaylistView.vue";

const routes = [
  {
    path: '/new-user',
    name: LOGIN_NAME,
    component: LoginView,
  },
  {
    path: `/playlist/:id/`,
    name: PLAYLIST_SELECTED_NAME,
    component: PlaylistView,
  },
  {
    path: '/new-playlist',
    name: CREATE_NEW_PLAYLIST_NAME,
    component: CreatePlaylistView
  },
  {
    path: '/',
    name: PLAYLIST_VIEW,
    component: PlaylistSelectionView,
  },
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

export default API
