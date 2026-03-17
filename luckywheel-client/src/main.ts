import '@/assets/css/main.css'
import {createApp} from 'vue'
import {createPinia} from 'pinia'

import App from './App.vue'
import API from './router'
async function prepareApp() {
  if (import.meta.env.DEV) {
    const EnvironmentService = await import("@/services/environment/environment.service.ts");
    if (!EnvironmentService.default.isAspireDevelopment()) {
      const {worker} = await import('@/mocks/browser');
      return worker.start();
    }
  }
}


const app = createApp(App);

prepareApp().then(() => {
  app.use(createPinia())
  app.use(API.router)

  app.mount('#app')
})
