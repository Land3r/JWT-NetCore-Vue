
const routes = [
  {
    name: '',
    path: '/',
    component: () => import('layouts/EmptyLayout.vue'),
    children: [
      {
        name: 'LoginPage',
        path: '',
        component: () => import('pages/user/LoginPage.vue')
      }
    ]
  },
  {
    path: '/user',
    component: () => import('layouts/MainLayout.vue'),
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
