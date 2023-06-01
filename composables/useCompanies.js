export default () => {

    const getCompanies = async () => {
        const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Azienda/GetCompanies', {
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

    const getCompany = async (getCompanyData) => {
        const { data, pending, error, refresh } = await useFetch('https://progettoeasynet.azurewebsites.net/Azienda/GetCompany', {
        lazy: true,  
        server: false,
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        method: 'GET',
        body: JSON.stringify(getCompanyData),
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
getCompanies,
getCompany

}

}
