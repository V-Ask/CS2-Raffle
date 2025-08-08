import {defineStore} from "pinia";
import {type Component} from "vue";

export const useDialogStore = defineStore('dialog', {
  state: () => ({
    open: false,
    headerText: '',
    content: undefined as Component | undefined,
  }),
  getters: {
    isDialogOpen(state) {
      return state.open;
    },

    getDialog(state) {
      return {
        headerText: state.headerText,
        content: state.content,
      };
    }
  },
  actions: {
    showDialog(headerText: string, component: Component) {
      this.headerText = headerText;
      this.content = component;
    },
  }
});
