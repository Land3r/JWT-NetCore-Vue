import {
  SET_TOKEN, UNSET_TOKEN,
  SET_USER, UNSET_USER,
  SET_REDIRECT_TO, UNSET_REDIRECT_TO
} from './types'

export const mutations = {
  [SET_TOKEN] (state, token) {
    state.token = token
  },
  [UNSET_TOKEN] (state) {
    state.token = null
  },
  [SET_USER] (state, user) {
    state.user = { ...user }
  },
  [UNSET_USER] (state) {
    state.user = null
  },
  [SET_REDIRECT_TO] (state, route) {
    state.redirectTo = route
  },
  [UNSET_REDIRECT_TO] (state) {
    state.redirectTo = null
  }
}
