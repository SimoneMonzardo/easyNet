export default () => {

    const getPosts = (params = {}) => {
        return new Promise(async (resolve, reject) => {
            try {
                const response = await useFetchApi('/api/tweets', {
                    method: 'GET',
                    params
                })

                resolve(response)
            } catch (error) {
                reject(error)
            }
        })
    }

}