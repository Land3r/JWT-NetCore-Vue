
const routes = [
  {
    name: '',
    path: '/user',
    component: () => import('layouts/EmptyLayout.vue'),
    meta: {
      requiresAuth: false
    },
    children: [
      {
        name: 'LoginPage',
        path: '/login',
        component: () => import('pages/user/LoginPage.vue')
      },
      {
        name: 'RegisterPage',
        path: '/register',
        component: () => import('pages/user/RegisterPage.vue')
      },
      {
        name: 'ForgotPasswordPage',
        path: '/forgotpassword',
        component: () => import('pages/user/ForgotPasswordPage.vue')
      },
      {
        path: '/',
        redirect: { name: 'LoginPage' }
      }
    ]
  },
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        name: 'IndexPage',
        path: '/',
        component: () => import('pages/Index.vue')
      }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
