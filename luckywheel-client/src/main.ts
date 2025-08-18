import '@/assets/css/main.css'
import {createApp} from 'vue'
import {createPinia} from 'pinia'

import App from './App.vue'
import router from './router'

async function prepareApp() {
  if (import.meta.env.DEV) {
    const {worker} = await import('@/mocks/browser');
    return worker.start().then();
  }
}

const app = createApp(App)

prepareApp().then(() => {
  app.use(createPinia())
  app.use(router)

  app.mount('#app')
})
