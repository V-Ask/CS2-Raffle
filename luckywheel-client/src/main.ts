import '@/assets/css/main.css'
import {createApp} from 'vue'
import {createPinia} from 'pinia'

import App from './App.vue'
import API from './router'

async function prepareApp() {
  const shouldUseMocks = import.meta.env.DEV && !import.meta.env.VITE_USE_REAL_API;

  if (shouldUseMocks) {
    const {worker} = await import('@/mocks/browser');
    return worker.start().then();
  }
}

const app = createApp(App)

prepareApp().then(() => {
  app.use(createPinia())
  app.use(API.router)

  app.mount('#app')
})
