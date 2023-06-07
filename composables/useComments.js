export default () => {
  const getCommentsOfAPost = async (getCommentsOfAPostData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Comments/GetCommentsOfAPost', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getCommentsOfAPostData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  const getComment = async (getCommentData) => {
    await useFetch('https://progettoeasynet.azurewebsites.net/Comments/GetComments', {
      lazy: true,
      server: false,
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      method: 'GET',
      body: JSON.stringify(getCommentData),
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
      },
    })
  }

  return {
    getCommentsOfAPost,
    getComment
  }
}