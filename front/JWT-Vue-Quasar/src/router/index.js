import Vue from 'vue'
import VueRouter from 'vue-router'

import routes from './routes'
import UserService from 'services/UserService'

Vue.use(VueRouter)

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation
 */

export default function (/* { store, ssrContext } */) {
  const Router = new VueRouter({
    scrollBehavior: () => ({ x: 0, y: 0 }),
    routes,

    // Leave these as is and change from quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    mode: process.env.VUE_ROUTER_MODE,
    base: process.env.VUE_ROUTER_BASE
  })

  const userService = new UserService()

  Router.beforeEach((to, from, next) => {
    // If user hits a page that requires auth, redirects him to the login page.
    if (to.matched.some(record => record.meta.requiresAuth)) {
      if (!userService.isConnected()) {
        next({
          path: '/login',
          params: { nextUrl: to.fullPath }
        })
      } else {
        next()
      }
    } else {
      next()
    }
  })
  return Router
}
