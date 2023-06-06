<!-- Main modal -->
<template>
  <div id="forget-modal" tabindex="-1" aria-hidden="true" class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
    <div class="relative w-full max-w-2xl max-h-full">
      <!-- Modal content -->
      <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 flex flex-row">
        <div class="bg-gray-200 dark:bg-gray-800 flex-col content-center rounded-l-lg w-64 p-5 text-gray-900 justify-evenly hidden sm:flex">
          <h6 class="mx-auto text-4xl font-semibold text-violet-600">Mouzone</h6>
          <img src="~/public/logo.png" class="mt-3 h-15 rounded-full" alt="Mouzone Logo" />
        </div>
        <div class="w-full">
          <button
            type="button"
            class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
            data-modal-hide="forget-modal">
            <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
              <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path>
            </svg>
            <span class="sr-only">Chiudi</span>
          </button>
          <div class="px-6 py-6 lg:px-8">
            <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">Recupero password</h3>
            <div class="relative my-10">
              <input
                type="email"
                id="forget-email"
                name="email"
                class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                placeholder=" "
                required />
              <label for="forget-email" class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">
                Email
              </label>
            </div>
            <button
              type="submit" 
              @click="executeRequest()"
              class="w-full text-white bg-violet-600 hover:bg-violet-700 focus:ring-4 focus:outline-none focus:ring-violet-300 font-medium rounded-xl text-sm px-5 py-2.5 text-center dark:bg-violet-700 dark:hover:bg-violet-700 dark:focus:ring-violet-800">
              Richiedi password
            </button>
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
  name: "ForgetPopup",
  methods: {
    closeModal() {
      const options = {};

      const forgetElement = document.getElementById('forget-modal');
      const forgetModal = new Modal(forgetElement, options);
      forgetModal.hide();
    },
    // TODO: We don't have an API to handle 'Forget password'
    async executeRequest() {
      // const { forget } = useAuth();
      const options = {};

      const forgetElement = document.getElementById('authentication-modal');
      const forgetModal = new Modal(forgetElement, options);
      forgetModal.hide();

      var data = {
        email: document.getElementById('forget-email').value,
      };

      // API Call
    }
  }
}
</script>
