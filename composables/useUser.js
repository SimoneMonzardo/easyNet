export default () => {
  const getUserFollowers = async (getUserFollowersData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/User/GetUserFollowers', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getUserFollowersData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getFollowers = async () => {
    await useFetch('https://progettoeasynet.azurewebsites.net/User/GetFollowers', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getUserFollowedList = async (getUserFollowedListData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/User/GetUserFollowedList', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getUserFollowedListData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getFollowed = async () => {
    await useFetch('https://progettoeasynet.azurewebsites.net/User/GetFollowed', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const follow = async (followData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/User/Follow', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(followData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const unfollow = async (unfollowData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/User/Unfollow', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'POST',
      body: JSON.stringify(unfollowData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
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