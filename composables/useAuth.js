export default () => {
  const changePassword = async (changePasswordData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Auth/changePassword', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(changePasswordData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      }
    })
  }

  const deleteUser = async () => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Auth/DeleteUser', {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      method: 'DELETE',
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      }
    })
  }

  const editUserData = async (editUserDataData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Auth/editUserData', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(editUserDataData),
      onRequest({ request, options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getUserData = async () => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Auth/GetUserData', {
      lazy: true,
      server: false,
      method: 'GET',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
      onResponse({ response }) {
        username = response._data.userName;
        sessionStorage.setItem('backupName', response._data.name);
        sessionStorage.setItem('backupSurname', response._data.surname);
        sessionStorage.setItem('backupGender', response._data.gender);
        sessionStorage.setItem('backupBirthDate', response._data.birthdate);
        sessionStorage.setItem('backupProfilePicture', response._data.profilePicture);
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