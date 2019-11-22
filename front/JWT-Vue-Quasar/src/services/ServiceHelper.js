/**
* Generic response handler.
* @param {Axios WebResponse} response The response to handle.
*/
export function handleResponse (response) {
  return Promise.resolve(response.data)
}

/**
* Generic response error handler.
* @param {Axios WebResponse} error The error to handle.
*/
export function handleError (error) {
  if (error.response) {
    // The request was made and the server responded with a status code
    //   that falls out of the range of 2xx
    const result = {
      ok: false,
      status: error.response.status,
      statusText: error.response.statusText,
      data: error.response.data
    }
    return Promise.reject(result)
  } else if (error.request) {
    // The request was made but no response was received
    // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
    // http.ClientRequest in node.js
    const result = {
      ok: false,
      status: 404,
      statusText: 'No response was received from the server',
      data: null
    }
    return Promise.reject(result)
  } else {
    // Something happened in setting up the request that triggered an Error
    console.log('Error', error.message)
    const result = {
      ok: false,
      status: null,
      statusText: null,
      data: null
    }
    return Promise.reject(result)
  }
}
