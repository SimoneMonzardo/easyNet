export default () => {
  const getPostOfUser = async (getPostOfUserData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Post/GetPostOfUser', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getPostOfUserData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getAllPosts = async () => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Post/GetAllPosts', {
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

  const getpostById = async (getpostByIdData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Post/GetPostById', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getpostByIdData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getAllPostsOfFollowed = async (getAllPostsOfFollowedData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Post/GetAllPostsOfFollowed', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getAllPostsOfFollowedData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      }
    })
  }

  const deletePost = async (deletePostData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Post/DeletePost', {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      method: 'DELETE',
      body: JSON.stringify(deletePostData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;

      },
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