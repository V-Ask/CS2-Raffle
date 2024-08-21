import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './css/main.css'

import { library } from '@fortawesome/fontawesome-svg-core'

import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faTrash, faGamepad, faPlay } from '@fortawesome/free-solid-svg-icons'
library.add(faTrash, faGamepad, faPlay);

const app = createApp(App)
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(router)
app.mount('#app')
