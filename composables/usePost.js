export default () => {
    
    const getPostOfUser = async (getPostOfUserData) => {
        const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Auth/register', {
        lazy: true,  
        server: false,
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        method: 'GET',
        body: JSON.stringify(getPostOfUserData),
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

return {
    getPostOfUser,
}
}