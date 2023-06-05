<template>
    <div id="followers-modal" tabindex="-1" aria-hidden="true"
        class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
        <div class="relative w-full max-w-lg max-h-full">
            <!-- Modal content -->

            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 ">
                <!-- Modal header -->
                <div class="flex items-center justify-between p-5 border-b rounded-t dark:border-gray-600">
                    <h3 class="text-xl font-medium text-gray-900 dark:text-white">
                        followers
                    </h3>
                    <div class="relative m-2">
                        <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                            <svg class="w-5 h-5 text-gray-500 dark:text-gray-400" aria-hidden="true" fill="currentColor"
                                viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd"
                                    d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                                    clip-rule="evenodd"></path>
                            </svg>
                        </div>
                        <input v-on:input="filterUsers()" type="text" v-model="input" id="table-search-users-followers"
                            class="block p-2 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg w-80 bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                            placeholder="Search for users">

                    </div>
                    <button type="button"
                        class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white"
                        data-modal-hide="followers-modal">
                        <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20"
                            xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd"
                                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                                clip-rule="evenodd"></path>
                        </svg>
                        <span class="sr-only">Close modal</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="p-6 space-y-6 h-96 overflow-y-auto mb-5">
                    <div v-if="pending">

                        <div v-for="index in 4" :key="index" class="animate-pulse flex space-x-4 mb-3">
                            <div class="rounded-full bg-slate-700 h-10 w-10 mt-1"></div>
                            <div class="flex-1 space-y-6 py-1">

                                <div class="grid grid-cols-3 gap-4">
                                    <div class="h-2 bg-slate-700 rounded col-span-1"></div>
                                    <div class="h-0 bg-slate-700 rounded col-span-1"></div>
                                    <div class="h-2 bg-slate-700 rounded col-span-1"></div>
                                </div>
                                <div class="grid grid-cols-3 gap-4">
                                    <div class="h-2 bg-slate-700 rounded col-span-1"></div>

                                </div>
                                <hr>
                            </div>
                        </div>
                    </div>
                    <div v-else>
                        <table id="followers-table" class="w-full text-sm text-left text-gray-500 dark:text-gray-400 ">

                            <tbody>

                                <tr v-for="user in filteredUsers()"
                                    class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                    <th scope="row"
                                        class="flex items-center px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white">
                                        <img class="w-10 h-10 rounded-full" :src="user.profilePicture" alt="Jese image">
                                        <div class="pl-3">
                                            <a class="text-base font-semibold">{{ user.username }}</a>
                                            <div v-if="user.companyName == ''" class="font-normal text-gray-500">utente
                                                privato</div>
                                            <div v-else class="font-normal text-gray-500">{{ user.companyName }}</div>
                                        </div>
                                    </th>
                                    <th>
                                        <button v-if="user.followed" @click="unfollow(user)"
                                            class="relative inline-flex items-center justify-center p-0.5 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-purple-600 to-blue-500 group-hover:from-purple-600 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800">
                                            <span
                                                class="relative px-4 py-1 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-opacity-0">Segui
                                                già</span>
                                        </button>
                                        <button v-else @click="follow(user)"
                                            class="text-white bg-gradient-to-br from-purple-600 to-blue-500 hover:bg-gradient-to-bl focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 font-medium rounded-lg text-sm px-4 py-1 text-center">
                                            Segui
                                        </button>
                                    </th>
                                </tr>
                                <div class="item error" v-if="input && !filteredUsers().length">
                                    <p>No results found!</p>
                                </div>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
</template>
<script>

export default {
    name: 'FollowersPopup',
    props: ['followedBy'],
    async setup(props) {
        const { data: users, pending } = await useFetch(`https://progettoeasynet.azurewebsites.net/User/GetUserFollowers?userName=${props.followedBy}`, {
            lazy: true,
            server: false,
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Authorization': ''
            },
            onRequest({ request, options }) {
                options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
            }
        });
        return {
            users,
            pending
        }
    },
    methods: {
        async unfollow(user) {
            await useFetch(`https://progettoeasynet.azurewebsites.net/User/Unfollow?userName=${user.username}`, {
                method: 'POST',
                body: JSON.stringify({ userName: user.username }),
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': ''
                },
                onRequest({ request, options }) {
                    options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
                }
            });
            user.followed = false;
        },
        async follow(user) {
            await useFetch(`https://progettoeasynet.azurewebsites.net/User/Follow?userName=${user.username}`, {
                method: 'POST',
                body: JSON.stringify({ userName: user.username }),
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': ''
                },
                onRequest({ request, options }) {
                    options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
                }
            });
            user.followed = true;
        },

        filteredUsers() {

            var input = document.getElementById("table-search-users-followers").value

            if (input != "" && input != null) {

                var tempUsers = [];

                for (var item of this.users) {



                    if (item.username.toLowerCase().includes(input.toLowerCase())) {

                        tempUsers.push(item);

                    }

                }

                return tempUsers;

            }

            else {

                return this.users;

            }

        },

        filterUsers() {
            this.$forceUpdate();

        }

    }
}



</script>
