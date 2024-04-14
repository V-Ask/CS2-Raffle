import { createApp } from 'vue'
import Raffle from './components/Raffle.vue'
import { createRouter, createMemoryHistory } from 'vue-router'
import App from './App.vue'

const routes = [
    { path: '/',
      name: 'Raffle',
      component: Raffle
    }
];

const router = createRouter({
    history: createMemoryHistory(),
    routes,
});

createApp(App)
    .use(router)
    .mount('#app')
