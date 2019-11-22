import {
  SET_TOKEN, UNSET_TOKEN,
  SET_USER, UNSET_USER,
  SET_REDIRECT_TO, UNSET_REDIRECT_TO
} from './types'

export function connectUser (context, { user, token }) {
  context.commit(SET_TOKEN, token)
  context.commit(SET_USER, user)
}

export function setToken (context, token) {
  context.commit(SET_TOKEN, token)
}

export function unsetToken (context) {
  context.commit(UNSET_TOKEN)
}

export function setUser (context, user) {
  context.commit(SET_USER, user)
}

export function unsetUser (context) {
  context.commit(UNSET_USER)
}

export function setRedirectTo (context, route) {
  context.commit(SET_REDIRECT_TO, route)
}

export function unsetRedirectTo (context) {
  context.commit(UNSET_REDIRECT_TO)
}
