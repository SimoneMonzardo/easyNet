<template>
  <div id="company-modal" tabindex="-1" aria-hidden="true"
    class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
    <div class="relative w-full max-w-3xl max-h-full py-12">
      <!-- Modal content -->
      <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 flex flex-row">
        <div
          class="bg-gray-200 dark:bg-gray-800 flex-col content-center rounded-l-lg w-64 p-5 text-gray-900 justify-evenly hidden sm:flex">
          <h6 class="mx-auto text-4xl font-semibold text-violet-500">easyNet</h6>
          <img v-if="isDark" src="~/public/muznet-white.png" class="mt-3 h-15 rounded-full" alt="MuzNet Logo" />
          <img v-else-if="!isDark" src="~/public/muznet-black.png" class="mt-3 h-15 rounded-full" alt="MuzNet Logo" />
        </div>
        <div class="w-full">
          <button type="button"
            class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
            data-modal-hide="company-modal">
            <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20"
              xmlns="http://www.w3.org/2000/svg">
              <path fill-rule="evenodd"
                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                clip-rule="evenodd"></path>
            </svg>
            <span class="sr-only">Chiudi</span>
          </button>
          <div class="px-6 py-6 lg:px-8">
            <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">
              Crea un azienda
            </h3>
            <form class="space-y-6 py-6" action="#" @submit.prevent="submit" method="post" enctype="multipart/form-data">
              <div class="grid gap-4 mb-4 sm:grid-cols-2 sm:gap-6 sm:mb-5">
                <div class="relative">
                  <input type="text" id="name" name="name"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-500 focus:outline-none focus:ring-0 focus:border-violet-600 peer"
                    placeholder=" " required />
                  <label for="name"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-600 peer-focus:dark:text-violet-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1">Nome
                  </label>
                </div>
              </div>
              <div class="relative">
                <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white" for="user_avatar">Immagine
                  profilo azienda</label>
                <input v-on:change="updateDocument($event.target.files, 'document1')"
                  class="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
                  aria-describedby="user_avatar_help" id="input_document1" type="file" name="img" accept="image/png"
                  required>
                <div class="mt-1 text-sm text-gray-500 dark:text-gray-300" id="user_avatar_help">È necessario inserire un
                  documento
                </div>
              </div>
              <div class="relative">
                <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white" for="user_avatar">Visura
                  camerale (Pdf)</label>
                <input v-on:change="updateDocument($event.target.files, 'document2')"
                  class="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
                  aria-describedby="user_avatar_help" id="input_document" type="file" accept="application/pdf" required>
                <div class="mt-1 text-sm text-gray-500 dark:text-gray-300" id="user_avatar_help">La Visura Camerale serve
                  a
                  confermare la tua identità come rappresentante dell'azienda
                </div>
              </div>
              <fieldset>
                <legend class="sr-only">Checkbox variants</legend>
                <div class="flex items-center mb-4">
                  <input 
                    id="checkbox-1" 
                    type="checkbox" 
                    value="" 
                    required 
                    @click="() => { this.accepted = !this.accepted; }"
                    class="w-4 h-4 text-violet-600 bg-gray-100 border-gray-300 rounded focus:ring-violet-500 dark:focus:ring-violet-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
                  <label for="checkbox-1" class="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300">Dichiaro di
                    accettare i Termini & Condizioni</label>
                </div>
              </fieldset>
              <div class="flex flex-row-reverse">
                <button
                  :disabled="!this.accepted" 
                  type="submit" 
                  v-on:click="handleCreation()"
                  class="w-full text-white bg-violet-600 hover:bg-violet-700 focus:ring-4 focus:outline-none focus:ring-violet-300 font-medium rounded-xl text-sm px-5 py-2.5 text-center dark:bg-violet-700 dark:hover:bg-violet-700 dark:focus:ring-violet-800 disabled:bg-violet-300">
                  Crea azienda
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
  
<script>
export default {
  name: "createCompany",
  data: () => ({ 
    accepted: false, 
    docsData: new FormData() 
  }),
  methods: {
    async api() {
      var companyData = {
        companyId: 0,
        companyName: document.getElementById("name").value,
        profilePicture: "",
        bot: {},
        documents: []
      };

      await useFetch(`https://progettoeasynet.azurewebsites.net/Azienda/RequestToAddCompany`, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          'Authorization': ""
        },
        method: "POST",
        body: JSON.stringify(companyData),
        onRequest({ options }) {
          options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
        },
        onResponse({ response }) {
          if (response.ok) {
            const optionsModal = {};
            const createCompanyElement = document.getElementById('company-modal');
            const createCompanyModal = new Modal(createCompanyElement, optionsModal);
            createCompanyModal.hide();
          }
        }
      });
    },
    async postDocuments() {
      await useFetch('https://progettoeasynet.azurewebsites.net/Azienda/postDocuments', {
        method: 'POST',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        body: this.docsData,
        onRequest({ options }) {
          options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
        }
      });
    },
    updateDocument(_document, name) {
      if (this.docsData.get(name) !== null) {
        this.docsData.delete(name);
      }

      this.docsData.append(name, _document[0]);
    },
    async handleCreation() {
      await this.api();
      await this.postDocuments();
    }
  }
}
</script>

<script setup>
import { useDark } from "@vueuse/core";

const isDark = useDark();
</script>
  