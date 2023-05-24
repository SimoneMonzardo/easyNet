export default() => {

    //to do: mettere gli auth token negli header

    const register = ({email, username, name, surname, dateOfBirth, gender, profilePicture, phoneNumber, password, role})=>{
        return new Promise(async (resolve, reject)=> {
            try {
                const data = await $fetch('/api/auth/register', {
                    method: 'POST',
                    body: {
                        email,
                        username,
                        name,
                        surname,
                        dateOfBirth,
                        gender,
                        profilePicture,
                        phoneNumber,
                        password,
                        role
                    }

                })
                resolve(true)
            }catch (error){
                reject(error)
            }
        })
    }

    const login = ({ username, password }) => {
        return new Promise(async (resolve, reject) => {
            try {
                const response = await $fetch('/api/auth/login', {
                    method: 'POST',
                    body: {
                        username,
                        password
                    }
                })
                
                // to do: mettere gli auth token nel localstorage
                localStorage.setItem('token',response.token)

                resolve(response)
            } catch (error) {
                reject(error)
            }
        })
    }

    const changePassword = ({oldPassword, newPassword})=>{
        return new Promise(async (resolve, reject) => {
            try {
                const response = await $fetch('/api/auth/changepassword', {
                    method: 'POST',
                    body: {
                        oldPassword,
                        newPassword
                    }
                })
                resolve(response)
            } catch (error) {
                reject(error)
            }
        })
    }

    //sistemare
    const deleteUser = ()=>{


    }

    const getUserData = ()=> {
        return new Promise(async (resolve, reject) => {
            try {
                const response = await $fetch('/api/auth/getuserdata',{
                method: 'GET'
                })
                resolve(response)
            } catch (error) {
                reject(error)
            }
        })
    }
        return{
            useAuthToken,
            register,
            login,
            changePassword,
            deleteUser,
            getUserData
        }
}