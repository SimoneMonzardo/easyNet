export default() => {

    //to do: mettere gli auth token negli header

    const register = async ({email, username, name, surname, dateOfBirth, gender, profilePicture, phoneNumber, password, role})=>{
        const { data, pending, error, refresh } = await useFetch('/api/auth/register', {
            method : 'POST',
            body: {
                         email,
                         username,
                         name,
                         surname,
                         dateOfBirth,
                         gender,
                         profilePicture,
                         phoneNumber,
                         password,
                         role
            },
            onRequest({ request, options }) {
              // Set the request headers

            },
            onRequestError({ request, options, error }) {
              // Handle the request errors
            },
            onResponse({ request, response, options }) {
              // Process the response data
              localStorage.setItem('token', response._data.token)
              return response
            },
            onResponseError({ request, response, options }) {
              // Handle the response errors
            }
        })
    }

    const login = async ({username, password })=>{
        const { data, pending, error, refresh } = await useFetch('/api/auth/login', {
            method : 'POST',
            body: {
                         username,
                         password,
            },
            onRequest({ request, options }) {
              // Set the request headers

            },
            onRequestError({ request, options, error }) {
              // Handle the request errors
            },
            onResponse({ request, response, options }) {
              // Process the response data
              localStorage.setItem('token', response._data.token)
              return response
            },
            onResponseError({ request, response, options }) {
              // Handle the response errors
            }
        })
    }

    
    const changePassword = async ({oldPassword, newPassword})=>{
        const { data, pending, error, refresh } = await useFetch('/api/auth/changepassword', {
            method : 'POST',
            body: {
                oldPassword,
                newPassword
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

        
    const deleteUser = async ()=>{
        const { data, pending, error, refresh } = await useFetch('/api/auth/deleteUser', {
            method : 'POST',
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

            
    const getUserData = async ()=>{
        const { data, pending, error, refresh } = await useFetch('/api/auth/getUserData', {
            method : 'GET',
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

        return{
            register,
            login,
            changePassword,
            deleteUser,
            getUserData
        }
}