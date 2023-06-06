export default () => {

    const getCommentsOfAPost = async (getCommentsOfAPostData) => {
        const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Comments/GetCommentsOfAPost', {
        lazy: true,  
        server: false,
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        method: 'GET',
        body: JSON.stringify(getCommentsOfAPostData),
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

const getComment = async (getCommentData) => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Comments/GetComments', {
    lazy: true,  
    server: false,
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'GET',
    body: JSON.stringify(getCommentData),
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

return {
    getCommentsOfAPost,
    getComment
}
}