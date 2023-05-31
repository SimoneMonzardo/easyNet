export default () => {
    
    const getPostOfUser = async (getPostOfUserData) => {
        const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Post/GetPostOfUser', {
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

const getAllPosts = async () => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Post/GetAllPosts', {
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

const getpostById = async (getpostByIdData) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Post/GetPostById', {
    lazy: true,  
    server: false,
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'GET',
    body: JSON.stringify(getpostByIdData),
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

const getAllPostsOfFollowed = async (getAllPostsOfFollowedData) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Post/GetAllPostsOfFollowed', {
    lazy: true,  
    server: false,
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'GET',
    body: JSON.stringify(getAllPostsOfFollowedData),
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

const deletePost = async (deletePostData) => {
    const { data, pending, error, refresh } = await useFetch('https://localhost:44359/Post/DeletePost', {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      method: 'DELETE',
      body: JSON.stringify(deletePostData),
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
    getAllPosts,
    getpostById,
    getAllPostsOfFollowed,
    deletePost


}
}