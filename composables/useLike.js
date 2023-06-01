export default () => {


    const getLikedPost = async () => {
        const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Like/GetLikedPost', {
        lazy: true,  
        server: false,
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        method: 'GET',
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

return{
    getLikedPost,
}
}