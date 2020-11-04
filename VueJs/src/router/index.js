import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Clientes from '../components/Clientes.vue'
import Libros from '../components/Libros.vue'
import Ordenes from '../components/Ordenes.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/', name: 'home', component: Home,
    meta: {
      admin: true
    }
  },
  {
    path: '/clientes', name: 'clientes', component: Clientes,
    meta: {
      admin: true
    }
  },
  {
    path: '/libros', name: 'libros', component: Libros,
    meta: {
      admin: true
    }
  },
  {
    path: '/ordenes', name: 'ordenes', component: Ordenes,
    meta: {
      admin: true
    }
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
