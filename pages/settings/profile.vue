<!-- TODO: Add login/register modals -->
<!-- TODO: Check if user is logged -->
<!-- TODO: Submit changes -->
<!-- TODO: Cancel changes -->

<template>
  <div id="confirm-delete-modal" tabindex="-1" class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
    <div class="relative w-full max-w-md max-h-full">
      <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
        <button type="button" class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white" data-modal-hide="confirm-delete-modal">
            <XMarkIcon class="h-6 w-6 text-gray-500 dark:text-gray-400" />
            <span class="sr-only">Close modal</span>
        </button>
        <div class="p-6 text-center">
          <ExclamationCircleIcon class="mx-auto h-16 w-16 text-gray-500 dark:text-gray-400" />
          <h3 class="text-lg font-normal text-gray-500 dark:text-gray-400">Sei certo di voler cancellare l'account?</h3>
          
          <div class="my-5">
            <!-- TODO: Use username -->
            <label for="confirm-delete-text" class="text-left block mb-1 text-sm font-medium text-gray-900 dark:text-white">Scrivi <span class="text-red-600 font-semibold">elimina.utente</span> per confermare</label>
            <input v-on:keyup="validateConfirmDelete()" type="text" id="confirm-delete-text" class="block w-full p-2 text-gray-900 border border-gray-300 rounded-lg bg-gray-50 sm:text-xs focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
          </div>

          <button disabled data-modal-hide="confirm-delete-modal" type="button" id="confirm-delete-modal-confirm" class="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-semibold rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center mr-2 disabled:cursor-not-allowed disabled:bg-red-400">
            Sì, elimina
          </button>
          <button data-modal-hide="confirm-delete-modal" type="button" class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 rounded-lg border border-gray-200 text-sm font-semibold px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">No, annulla</button>
        </div>
      </div>
    </div>
  </div>

  <section class="bg-white dark:bg-gray-900 h-full">
    <SettingSelector />

    <div class="justify-center flex flex-col sm:flex-row gap-4 mx-10 md:mx-auto mt-12">
      <div class="w-40 lg:w-48 flex flex-col items-center gap-2 lg:pt-4 mx-auto sm:mx-0">
        <img :src="imageUrl" class="w-32 h-32 lg:h-40 lg:w-40 rounded-full border border-gray-600 dark:border-gray-100" />
        <button type="button"
          class="w-[calc(100%-2rem)] text-black bg-gray-300 hover:bg-gray-400 focus:outline-none focus:ring-4 focus:ring-blue-300 font-semibold rounded-full text-lg py-2 text-center mr-2 mb-2 dark:text-white dark:bg-gray-700 dark:hover:bg-gray-800 dark:focus:ring-blue-800">Modifica</button>
      </div>
      <div class="w-full md:w-1/2 2xl:w-1/3 py-3">
        <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <div class="relative">
            <input type="email" id="updateEmail" name="email"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              readonly required placeholder=" " />
            <label for="updateEmail"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Email</label>
          </div>

          <div class="relative">
            <input type="tel" id="updatePhoneNumber" name="phoneNumber"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              readonly required placeholder=" " />
            <label for="updatePhoneNumber"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Numero
              di telefono</label>
          </div>

          <div class="relative">
            <input type="text" id="updateName" name="name"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder=" " required />
            <label for="updateName"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Nome</label>
          </div>

          <div class="relative">
            <input type="text" id="updateSurname" name="surname"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder=" " required />
            <label for="updateSurname"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Cognome</label>
          </div>

          <div class="relative">
            <input type="date" id="updateBirthDate" name="dateOfBirth"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder=" " required />
            <label for="updateBirthDate"
              class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Data
              di nascita
            </label>
          </div>

          <div>
            <select id="updateGender" required name="gender"
              class="px-2.5 pb-2.5 pt-4 bg-white border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-900 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
              <option selected disabled class="disabled:text-gray-500 dark:disabled:text-gray-400">Inserisci il genere
              </option>
              <option value="male">Maschio</option>
              <option value="female">Femmina</option>
            </select>
          </div>

          <div class="sm:col-span-2 relative">
            <label for="updateDescription"
                class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Descrizione</label>
            <textarea id="updateDescription" rows="5"
              class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
              placeholder="Scrivi una descrizione del profilo..."></textarea>
          </div>    
        </div>
        <div class="w-full flex flex-wrap gap-4 justify-between mt-5">
          <div class="inline-flex">
            <button class="w-24 lg:w-28 bg-gray-300 hover:bg-gray-400 text-gray-1000 font-bold py-2 rounded-l-full">Annulla</button>
            <button class="w-24 lg:w-28 bg-green-400 hover:bg-green-500 text-gray-1000 font-bold py-2 rounded-r-full">Salva</button>
          </div>
          <button @click="resetModal()" data-modal-target="confirm-delete-modal" data-modal-toggle="confirm-delete-modal" class="bg-red-500 hover:bg-red-600 text-gray-1000 font-bold py-2 px-4 rounded-full">Elimina profilo</button>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { ExclamationCircleIcon } from "@heroicons/vue/24/outline";
import { XMarkIcon } from "@heroicons/vue/24/outline";

export default {
  props: {
    profileImage: String,
  },
  computed: {
    imageUrl() {
      return this.profileImage;
    }
  },
  components: {
    ExclamationCircleIcon,
    XMarkIcon
  },
  methods: {
    validateConfirmDelete() {
      const value = document.getElementById('confirm-delete-text').value;
      const confirmButton = document.getElementById('confirm-delete-modal-confirm');

      // TODO: Use username
      if (value === `elimina.${'utente'}`) {
        confirmButton.removeAttribute('disabled');
      } else {
        confirmButton.setAttribute('disabled', '');
      }
    },
    resetModal() {
        document.getElementById('confirm-delete-text').value = '';
        document.getElementById('confirm-delete-modal-confirm').setAttribute('disabled', '');
    }
  }
}
</script>
