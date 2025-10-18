import {
  createWebHistory,
} from 'vue-router'
import LoginView from "@/views/LoginView.vue";
import {
  ADD_MAP_TO_PLAYLIST_NAME,
  CREATE_NEW_PLAYLIST_NAME,
  LOGIN_NAME, NOT_FOUND_NAME, PLAYLIST_SELECTED_NAME, PLAYLIST_VIEW
} from "@/helpers/constants/routing.ts";
import PlaylistView from "@/views/PlaylistView.vue";
import PlaylistSelectionView from "@/views/PlaylistSelectionView.vue";
import {GuardedRouter} from "@/router/guarded-router.ts";
import {inverseGuardFn} from "@/guards/guard-fn.ts";
import {authGuardFn} from "@/guards/auth-guard.ts";
import NotFoundView from "@/views/NotFoundView.vue";
import CreatePlaylistView from "@/views/CreatePlaylistView.vue";
import {useLoadingStore} from "@/stores/loading.store.ts";
import AddNewMapView from "@/views/AddNewMapView.vue";

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
    children: [
      {
        path: 'add',
        name: ADD_MAP_TO_PLAYLIST_NAME,
        component: AddNewMapView
      }
    ]
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

// Redirects to the playlist selection view if the user is attempting to access
// login page while authorized
// API.guardRoute(LOGIN_NAME, inverseGuardFn(authGuardFn), {
//   name: PLAYLIST_VIEW
// });

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
