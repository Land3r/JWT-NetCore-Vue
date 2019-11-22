import axios from 'axios'
import { LocalStorage } from 'quasar'

import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'AUTH': '/auth',
  'GET': ''
}

const localStorageKeys = {
  user: 'user',
  token: 'token'
}

/**
 * UserService class.
 */
export default class UserService {
  /**
   * Authenticates the user.
   * @param {string} username The user's username.
   * @param {string} password The user's password.
   */
  doAuthenticate (username, password) {
    const requestOptions = {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.AUTH),
      data: {
        username: username,
        password: password
      }
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Gets the current authenticated user.
   */
  getCurrentUser () {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.GET)
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Disconnects the user.
   */
  disconnect () {
    LocalStorage.remove(localStorageKeys.user)
    LocalStorage.remove(localStorageKeys.token)

    // Removes the token
    axios.defaults.headers.common['Authorization'] = null
  }

  /**
   * Connects the provided user to the application.
   * @param {User} user The user to connect.
   */
  connect (user) {
    const { token, password, ...otherUser } = user
    LocalStorage.set(localStorageKeys.user, { ...otherUser })
    LocalStorage.set(localStorageKeys.token, token)

    // Sets the token in axios calls.
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
  }

  /**
   * Gets whether or not the user is connected.
   */
  isConnected () {
    if (LocalStorage.getItem(localStorageKeys.user) != null && LocalStorage.getItem(localStorageKeys.token) != null) {
      return true
    } else {
      return false
    }
  }

  getUser () {
    return LocalStorage.getItem(localStorageKeys.user)
  }

  getToken () {
    return LocalStorage.getItem(localStorageKeys.token)
  }
}
