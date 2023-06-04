<!-- Main modal -->
<template>
    <div id="authentication-modal" tabindex="-1" aria-hidden="true"
        class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
        <div class="relative w-full max-w-2xl max-h-full">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 flex flex-row">
                <div
                    class="bg-gray-300 flex-col content-center rounded-l-lg w-64 p-5 text-gray-900 justify-evenly hidden sm:flex">
                    <h4 class="text-2xl font-semibold mx-auto">Bentornato</h4>
                    <h6 class="mx-auto text-4xl font-semibold">easyNet</h6>
                    <img src="https://flowbite.s3.amazonaws.com/logo.svg" class="mt-3 h-15 rounded-full"
                        alt="FlowBite Logo" />
                </div>
                <div class="w-full">
                    <button
                        id="close-login-modal-button" 
                        type="button"
                        class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
                        data-modal-hide="authentication-modal">
                        <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20"
                            xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd"
                                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                                clip-rule="evenodd"></path>
                        </svg>
                        <span class="sr-only">Chiudi</span>
                    </button>
                    <div class="px-6 py-6 lg:px-8">
                        <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">Log in</h3>
                        <form class="space-y-6" @submit.prevent="submit" method="post">
                            <div class="relative">
                                <input type="text" id="username" name="userName"
                                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" " required />
                                <label for="username"
                                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Username</label>
                            </div>
                            <div class="relative">
                                <input type="password" id="password" name="password"
                                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" " required />
                                <label for="password"
                                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Password</label>
                            </div>
                            <div class="flex justify-between">
                                <div class="flex items-start">
                                    <div class="flex items-center h-5">
                                        <input id="remember" type="checkbox" value=""
                                            class="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-blue-300 dark:bg-gray-600 dark:border-gray-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800">
                                    </div>
                                    <label for="remember"
                                        class="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300">Ricordami</label>
                                </div>
                                    <button type="button" @click="openForgetModal()"
                                        class="text-blue-700 hover:underline dark:text-blue-500">Password dimenticata?</button>
                            </div>
                            <button type="submit" @click="executeLogin()"
                                class="w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-xl text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Accedi</button>
                            <div class="text-sm font-medium text-gray-500 dark:text-gray-300">
                                Non sei registrato? <button type="button" @click="openRegisterModal()"
                                    class="text-blue-700 hover:underline dark:text-blue-500">Crea un account</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { Modal } from 'flowbite';
import useAuth from '~/composables/useAuth';

export default {
    name: "LoginPopup",
    methods: {
        //close current popup and open the selected next popup
        openRegisterModal() {
            const options = {};

            const loginElement = document.getElementById('authentication-modal');
            const loginModal = new Modal(loginElement, options);
            loginModal.hide();

            const registerElement = document.getElementById('register-modal');
            const registerModal = new Modal(registerElement, options);
            registerModal.show();
        },
        openForgetModal() {
            const options = {};

            const loginElement = document.getElementById('authentication-modal');
            const loginModal = new Modal(loginElement, options);
            loginModal.hide();

            const forgetElement = document.getElementById('forget-modal');
            const forgetModal = new Modal(forgetElement, options);
            forgetModal.show();
        },
        async executeLogin() {
            const { login } = useAuth();
            const options = {};

            const loginElement = document.getElementById('authentication-modal');
            const loginModal = new Modal(loginElement, options);
            loginModal.hide();

            const passwordInput = document.getElementById('password'); 
            const remindMe = document.getElementById('remember').checked;

            var data = {
                username: document.getElementById('username').value,
                password: passwordInput.value
            };

            passwordInput.value = '';

            await login(data, remindMe);
            this.$router.go();
        }
    }
}
</script>
