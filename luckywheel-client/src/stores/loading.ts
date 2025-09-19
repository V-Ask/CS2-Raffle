import {defineStore} from "pinia";

export const useLoadingStore = defineStore('loading', {
  state: () => ({
    lazyLoading: false,
    loading: false,
  }),
  getters: {
    isLoading(state) {
      return state.loading;
    },

    isLazyLoading(state) {
      return state.lazyLoading;
    }
  },
  actions: {
    startLoading() {
      this.loading = true;
    },

    stopLoading() {
      this.loading = false;
    },

    startLazyLoading() {
      this.lazyLoading = true;
    },

    stopLazyLoading() {
      this.lazyLoading = false;
    }
  }
})
