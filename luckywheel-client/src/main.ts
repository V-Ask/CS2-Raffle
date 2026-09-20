import '@/assets/css/main.css'
import '@/assets/css/variables.css'
import {createApp} from 'vue'
import {createPinia} from 'pinia'

import App from './App.vue'
import API from './router'
import EnvironmentService from "@/services/environment/environment.service.ts";

async function prepareApp() {
  const shouldUseMocks = EnvironmentService.isDevelopment() && !EnvironmentService.isAspireDevelopment();

  if (shouldUseMocks) {
    const {worker} = await import('@/mocks/browser');
    return worker.start().then();
  }
}


const app = createApp(App);

prepareApp().then(() => {
  app.use(createPinia())
  app.use(API.router)

  app.mount('#app')
})
