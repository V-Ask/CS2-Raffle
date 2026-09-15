import '@/assets/css/main.css'
import '@/assets/css/variables.css'
import {createApp} from 'vue'
import {createPinia} from 'pinia'

import App from './App.vue'
import API from './router'
import EnvironmentService from "@/services/environment/environment.service.ts";
import PrimeVue from "primevue/config";
import {PRIME_VUE_LICENSE} from "@/license/license-constants.ts";

async function prepareApp() {
  const shouldUseMocks = EnvironmentService.isDevelopment() && !EnvironmentService.isAspireDevelopment();

  if (shouldUseMocks) {
    const {worker} = await import('@/mocks/browser');
    return worker.start().then();
  }
}


const app = createApp(App);
app.use(PrimeVue, {
  /* PrimeVue configuration */
  license: PRIME_VUE_LICENSE
})

prepareApp().then(() => {
  app.use(createPinia())
  app.use(API.router)

  app.mount('#app')
})
