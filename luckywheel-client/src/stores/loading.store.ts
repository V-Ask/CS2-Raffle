import { defineStore } from "pinia";

export const useLoadingStore = defineStore("loading-store", {
  state() {
      return {
        loadingStarted: 0 as number,
      }
  },

  getters: {
    isLoading(state)  {
      return state.loadingStarted > 0;
    }
  },

  actions: {
    startLoading() {
      this.loadingStarted++;
      return () => {
        this.loadingStarted--;
      }
    }
  }
})
