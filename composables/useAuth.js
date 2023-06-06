export default () => {

  //to do: mettere gli auth token negli header
  // Bearer `authtoken`

  // const register = async (reigsterData) => {
  //   const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Auth/Register', {
  //     headers: {
  //       'Access-Control-Allow-Origin': '*'
  //     },
  //     method: 'POST',
  //     body: JSON.stringify(reigsterData),
  //     onRequest({ request, options }) {
  //       // Set the request headers
  //     },
  //     onRequestError({ request, options, error }) {
  //       // Handle the request errors
  //     },
  //     onResponse({ request, response, options }) {
  //       console.log(response);
        
  //     },
  //     onResponseError({ request, response, options }) {
  //       // Handle the response errors
  //     }
  //   });

  //   return data;
  // }

  
  const changePassword = async (changePasswordData) => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Auth/changePassword', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(changePasswordData),
      onRequest({ request, options }) {
        // Set the request headers
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
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
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Auth/DeleteUser', {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      method: 'DELETE',
      onRequest({ request, options }) {
        // Set the request headers
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;

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
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Auth/editUserData', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(editUserDataData),
      onRequest({ request, options }) {
        // Set the request headers
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
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
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Auth/GetUserData', {
      lazy: true,
      server: false,
      method: 'GET',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      onRequest({ request, options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
      onResponse({request, response, options}) {
        username = response._data.userName;
        console.log(response._data);
        sessionStorage.setItem('backupName', response._data.name);
        sessionStorage.setItem('backupSurname', response._data.surname);
        sessionStorage.setItem('backupGender', response._data.gender);
        sessionStorage.setItem('backupBirthDate', response._data.birthdate);
        sessionStorage.setItem('backupProfilePicture', response._data.profilePicture);
      },
      onResponseError() {
        // TODO: Handle error
      }
    });
  }

  return {
    changePassword,
    deleteUser,
    editUserData,
    getUserData
  }
}