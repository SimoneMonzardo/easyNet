export default () => {

  //to do: mettere gli auth token negli header
  // Bearer `authtoken`

  const register = async (reigsterData) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/Register', {
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
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/changePassword', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(changePasswordData),
      onRequest({ request, options }) {
        // Set the request headers
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
      }
    })
  }

  const deleteUser = async () => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/DeleteUser', {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      method: 'DELETE',
      onRequest({ request, options }) {
        // Set the request headers
        options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;

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

  const editUserData = async (editUserDataData) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/editUserData', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(editUserDataData),
      onRequest({ request, options }) {
        // Set the request headers
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
      }
    })
  }

  const getUserData = async () => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/GetUserData', {
      lazy: true,
      server: false,
      method: 'GET',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      onRequest({ request, options }) {
        options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
      },
      onResponse({request, response, options}) {
        username = response._data.userName;
        console.log(response._data);
        localStorage.setItem('backupName', response._data.name);
        localStorage.setItem('backupSurname', response._data.surname);
        localStorage.setItem('backupGender', response._data.gender);
        localStorage.setItem('backupBirthDate', response._data.birthdate);
        localStorage.setItem('backupProfilePicture', response._data.profilePicture);
      },
      onResponseError() {
        // TODO: Handle error
      }
    });
  }

  return {
    register,
    login,
    changePassword,
    deleteUser,
    editUserData,
    getUserData
  }
}