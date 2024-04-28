import { createRouter, createWebHistory } from 'vue-router'
import Raffle from '../components/RaffleComponent.vue'
import Ping from '../components/Ping.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{ path: '/', name: 'Raffle', component: Raffle },
            { path: '/test', name: 'Test', component: Ping}]
})

export default router
