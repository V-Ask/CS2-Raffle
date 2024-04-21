import { createRouter, createWebHistory } from 'vue-router'
import Raffle from '../views/RaffleView.vue'
import Ping from '../components/Ping.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{ path: '/', name: 'Raffle', component: Raffle },
            { path: '/test', name: 'Test', component: Ping},
            { path: '/:pathMatch(.*)', name: 'PageNotFound', component: Raffle}]
})

export default router
