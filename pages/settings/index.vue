<!-- TODO: Check if user is logged -->
<!-- TODO: Submit changes -->
<!-- TODO: Cancel changes -->
<!-- TODO: Use user data -->

<template>
  <section Modals>
    <!-- Confirm Delete Modal -->
    <div id="confirm-delete-modal" tabindex="-1"
      class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
      <div class="relative w-full max-w-md max-h-full">
        <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
          <button type="button"
            class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
            data-modal-hide="confirm-delete-modal">
            <XMarkIcon class="h-6 w-6 text-gray-500 dark:text-gray-400" />
            <span class="sr-only">Chiudi</span>
          </button>
          <div class="p-6 text-center">
            <ExclamationCircleIcon class="mx-auto h-16 w-16 text-gray-500 dark:text-gray-400" />
            <h3 class="text-lg font-normal text-gray-500 dark:text-gray-400">Sei certo di voler cancellare l'account?</h3>
            <div class="my-5">
              <label for="confirm-delete-text" class="text-left block mb-1 text-sm font-medium text-gray-900 dark:text-white">
                Scrivi <span class="text-red-600 font-semibold">elimina.{{ username }}</span> per confermare
              </label>
              <input @input="confirmDeleteText = $event.target.value" :value="confirmDeleteText" type="text"
                class="block w-full p-2 text-gray-900 border border-gray-300 rounded-lg bg-gray-50 sm:text-xs focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            </div>
            <button
              id="confirm-delete-modal-confirm"
              :disabled="confirmDeleteText !== `elimina.${username}`" 
              data-modal-hide="confirm-delete-modal" 
              @click="deleteUserAccount()"
              type="button" 
              class="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-semibold rounded-xl text-sm inline-flex items-center px-5 py-2.5 text-center mr-2 disabled:cursor-not-allowed disabled:bg-red-400">
              Sì, elimina
            </button>
            <button data-modal-hide="confirm-delete-modal" type="button" class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 rounded-xl border border-gray-200 text-sm font-semibold px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
              No, annulla
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Change Image Modal -->
    <UploadImagePopup />

    <!-- Login & Register Modals -->
    <RegisterPopup />
    <LoginPopup />
  </section>

  <section class="bg-white dark:bg-gray-900 h-full">
    <SettingSelector />

    <div class="justify-center flex flex-col sm:flex-row gap-4 mx-10 md:mx-auto mt-12">
      <div class="w-40 lg:w-48 flex flex-col items-center gap-2 lg:pt-4 mx-auto sm:mx-0">
        <!-- Loading -->
        <div v-if="pending" class="flex items-center justify-center w-32 h-32 lg:h-40 lg:w-40 bg-gray-300 rounded-full dark:bg-gray-700">
          <svg class="w-12 h-12 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor" viewBox="0 0 640 512">
            <path d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </div>

        <!-- Loaded Successfully -->
        <img v-else :src="user.profilePicture" class="w-32 h-32 lg:h-40 lg:w-40 rounded-full border border-gray-600 dark:border-gray-100" />

        <button type="button" data-modal-target="upload-image-modal" data-modal-toggle="upload-image-modal" :disabled="pending ? true : false"
          class="w-[calc(100%-2rem)] text-black bg-gray-300 hover:bg-gray-400 focus:outline-none focus:ring-4 focus:ring-blue-300 font-semibold rounded-xl text-lg py-2 text-center mr-2 mb-2 dark:text-white dark:bg-gray-700 dark:hover:bg-gray-800 dark:focus:ring-blue-800 disabled:bg-gray-100 disabled:cursor-not-allowed">Modifica</button>
      </div>
      <div class="w-full md:w-1/2 2xl:w-1/3 py-3">
        <!-- Loading -->
        <div v-if="pending">
          <div class="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-4"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[480px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[440px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[460px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px] mb-2.5"></div>
          <div class="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[480px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[440px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[460px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px]"></div>
        </div>

        <!-- Loaded Successfully -->
        <div v-else class="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <div class="relative">
            <input 
              type="email" 
              id="updateEmail" 
              name="email"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              readonly 
              placeholder=" " 
              :value="user.email" />
            <label for="updateEmail"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Email</label>
          </div>

          <div class="relative">
            <input 
              type="tel" 
              id="updatePhoneNumber" 
              name="phoneNumber"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              readonly 
              placeholder=" " 
              :value="user.phoneNumber" />
            <label for="updatePhoneNumber"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Numero
              di telefono</label>
          </div>

          <div class="relative">
            <input 
              type="text" 
              id="updateName" 
              name="name"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder=" " 
              required 
              :value="user.name" />
            <label for="updateName"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Nome</label>
          </div>

          <div class="relative">
            <input 
              type="text" 
              id="updateSurname" 
              name="surname"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder=" " 
              required 
              :value="user.surname"/>
            <label for="updateSurname"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Cognome</label>
          </div>

          <div class="relative">
            <input 
              type="date" 
              id="updateBirthDate" 
              name="dateOfBirth"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder=" " 
              required 
              :value="user.dateOfBirth"/>
            <label for="updateBirthDate"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Data
              di nascita
            </label>
          </div>

          <div>
            <select 
              id="updateGender" 
              required 
              name="gender"
              :value="user.gender"
              class="px-2.5 pb-2.5 pt-4 bg-white border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-900 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
              <option selected disabled class="disabled:text-gray-500 dark:disabled:text-gray-400">Inserisci il genere</option>
              <option value="male">Maschio</option>
              <option value="female">Femmina</option>
            </select>
          </div>
        </div>
        <div class="w-full flex flex-wrap gap-4 justify-between mt-5">
          <div class="inline-flex">
            <button id="cancel-changes" @click="cancelChanges()" :disabled="pending" class="w-24 lg:w-28 bg-gray-300 hover:bg-gray-400 text-gray-1000 font-bold py-2 rounded-l-xl disabled:bg-gray-100 disabled:cursor-not-allowed">Annulla</button>
            <button id="save-changes" @click="saveChanges()" :disabled="pending" class="w-24 lg:w-28 bg-green-400 hover:bg-green-500 text-gray-1000 font-bold py-2 rounded-r-xl disabled:bg-green-300 disabled:cursor-not-allowed">Salva</button>
          </div>
          <button id="delete-account" :disabled="pending" @click="resetModal()" data-modal-target="confirm-delete-modal" data-modal-toggle="confirm-delete-modal"
            class="bg-red-500 hover:bg-red-600 text-gray-1000 font-bold py-2 px-4 rounded-xl disabled:bg-red-400 disabled:cursor-not-allowed">Elimina profilo</button>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { ExclamationCircleIcon } from "@heroicons/vue/24/outline";
import { XMarkIcon } from "@heroicons/vue/24/outline";

export default {
  data: () => ({
    confirmDeleteText: '',
    user: {

    }
  }),
  components: {
    ExclamationCircleIcon,
    XMarkIcon
  },
  methods: {
    resetModal() {
      this.confirmDeleteText = '';
      document.getElementById('confirm-delete-modal-confirm').setAttribute('disabled', '');
    },
    async deleteUserAccount() {
      await useFetch('https://localhost:44359/Auth/DeleteUser', {
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        method: 'DELETE',
        onRequest({ request, options }) {
          options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
        }
      });
      this.$router.go('/');
    },
    cancelChanges() {
      document.getElementById('updateName').value = localStorage.getItem('backupName');
      document.getElementById('updateSurname').value = localStorage.getItem('backupSurname');
      document.getElementById('updateGender').value = localStorage.getItem('backupGender');
      document.getElementById('updateBirthDate').value = localStorage.getItem('backupBirthDate');
      //document.getAnimations('id').value = localStorage.getItem('backupProfilePicture');
    },
    async saveChanges() {
      var newUserInfo = {
        name: document.getElementById('updateName').value,
        surname: document.getElementById('updateSurname').value,
        gender: document.getElementById('updateGender').value,
        birthDate: document.getElementById('updateBirthDate').value,
        profilePicture: '' // TODO: Use -> document.getElementById('fileInputId').value
      };

      await useFetch('https://localhost:44359/Auth/editUserData', {
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        body: JSON.stringify(newUserInfo),
        method: 'POST',
        onRequest({ request, options }) {
          options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
        }
      });
      this.$router.go();
    }
  },
  mounted: function() {
    // const token = localStorage.getItem('token');
    // if (token === undefined || token === null || token === '') {
    //   this.$router.push ('/');
    // }
  }
}
</script>

<script setup>
var username = '';

// Use real API call
const { data: user, pending, error } = await useFetch('https://localhost:44359/Auth/GetUserData', {
  lazy: true,
  server: false,
  method: 'GET',
  headers: {
    'Access-Control-Allow-Origin': '*',
    'Authorization': ''
  },
  onRequest({ request, options }) {
    options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
  },
  onResponse({request, response, options}) {
    username = response._data.userName;
    console.log(response._data);
    localStorage.setItem('backupName', response._data.name);
    localStorage.setItem('backupSurname', response._data.surname);
    localStorage.setItem('backupGender', response._data.gender);
    localStorage.setItem('backupBirthDate', response._data.birthdate);
    localStorage.setItem('backupProfilePicture', response._data.profilePicture);
  },
  onResponseError() {
    // TODO: Handle error
  }
});
</script>
