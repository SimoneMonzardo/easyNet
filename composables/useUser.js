export default () => {
    
    const getUserFollowers = async (getUserFollowersData) => {
        const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/User/GetUserFollowers', {
        lazy: true,  
        server: false,
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        method: 'GET',
        body: JSON.stringify(getUserFollowersData),
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

const getFollowers = async () => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/User/GetFollowers', {
    lazy: true,  
    server: false,
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'GET',
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

const getUserFollowedList = async (getUserFollowedListData) => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/User/GetUserFollowedList', {
    lazy: true,  
    server: false,
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'GET',
    body: JSON.stringify(getUserFollowedListData),
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

const getFollowed = async () => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/User/GetFollowed', {
    lazy: true,  
    server: false,
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'GET',
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

const follow = async (followData) => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/User/Follow', {
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'POST',
    body: JSON.stringify(followData),
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

const unfollow = async (unfollowData) => {
    const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/User/Unfollow', {
    headers: {
        'Access-Control-Allow-Origin': '*'
    },
    method: 'POST',
    body: JSON.stringify(unfollowData),
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



return {
    getUserFollowers,
    getFollowers,
    getUserFollowedList,
    getFollowed,
    follow,
    unfollow
}
}