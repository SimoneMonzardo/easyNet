export default () => {

  //to do: mettere gli auth token negli header
  // Bearer `authtoken`

  const register = async (reigsterData) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/register', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(reigsterData),
      onRequest({ request, options }) {
        // Set the request headers
      },
      onRequestError({ request, options, error }) {
        // Handle the request errors
      },
      onResponse({ request, response, options }) {
        // Process the response data
        return response
      },
      onResponseError({ request, response, options }) {
        // Handle the response errors
      }
    })
  }

  const login = async (credentials) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/login', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(credentials),
      onRequest({ request, options }) {
        // Set the request headers
      },
      onRequestError({ request, options, error }) {
        // Handle the request errors
      },
      onResponse({ request, response, options }) {
        // Process the response data
        localStorage.setItem('logged', true);
        localStorage.setItem('username', response._data.username);
        localStorage.setItem('email', response._data.email);
        localStorage.setItem('profilePicture', response._data.profilePicture);
        localStorage.setItem('token', response._data.token);
        return response;
      },
      onResponseError({ request, response, options }) {
        // Handle the response errors
      }
    })
  }

  const changePassword = async (changePasswordData) => {
    const { data, pending, error, refresh } = await useFetch('/api/auth/changepassword', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(changePasswordData),
      onRequest({ request, options }) {
        options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
      },
      onRequestError({ request, options, error }) {
        // Handle the request errors
      },
      onResponse({ request, response, options }) {
        // Process the response data
        return response;
      },
      onResponseError({ request, response, options }) {
        // Handle the response errors
        return response.error;
      }
    })
  }


  const deleteUser = async () => {
    const { data, pending, error, refresh } = await useFetch('/api/auth/deleteUser', {
      method: 'POST',
      body: {
      },
      onRequest({ request, options }) {
        // Set the request headers

      },
      onRequestError({ request, options, error }) {
        // Handle the request errors
      },
      onResponse({ request, response, options }) {
        // Process the response data
        return response
      },
      onResponseError({ request, response, options }) {
        // Handle the response errors
      }
    })
  }


  const getUserData = async () => {
    const { data, pending, error, refresh } = await useFetch('/api/auth/getUserData', {
      method: 'GET',
      onRequest({ request, options }) {


      },
      onRequestError({ request, options, error }) {

      },
      onResponse({ request, response, options }) {

        return response
      },
      onResponseError({ request, response, options }) {

      }
    })
  }

  return {
    register,
    login,
    changePassword,
    deleteUser,
    getUserData
  }
}