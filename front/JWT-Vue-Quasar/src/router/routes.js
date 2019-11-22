
const routes = [
  {
    name: '',
    path: '/',
    component: () => import('layouts/EmptyLayout.vue'),
    meta: {
      requiresAuth: false
    },
    children: [
      {
        name: 'LoginPage',
        path: 'login',
        component: () => import('pages/user/LoginPage.vue')
      },
      {
        // Empty rule that requires auth. Allow for auto redirection to login page.
        name: 'Moc',
        path: '',
        meta: {
          requiresAuth: true
        }
      }
    ]
  },
  {
    path: '/user',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        path: '',
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
