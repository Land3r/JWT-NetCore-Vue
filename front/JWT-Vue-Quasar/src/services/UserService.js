import axios from 'axios'
import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'AUTH': '/auth'
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

    console.log(requestOptions)

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }
}
