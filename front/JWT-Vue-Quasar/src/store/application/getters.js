export function isLoggedIn (state) {
  return !!state.token && !!state.user
}

export function shouldBeRedirected (state) {
  return !!state.redirectTo
}
