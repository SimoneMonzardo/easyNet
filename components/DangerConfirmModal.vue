<template>
    <div id="danger-confirm-modal" tabindex="-1" aria-hidden="true"
      class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
      <div class="relative w-full max-w-lg max-h-full">
        <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
          <button type="button"
            class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
            data-modal-hide="danger-confirm-modal">
            <XMarkIcon class="h-6 w-6 text-gray-500 dark:text-gray-400" />
            <span class="sr-only">Chiudi</span>
          </button>
          <div class="px-6 py-6 lg:px-8">
            <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">{{ title }}</h3>
            <div class="space-y-6">
              <p>{{ description }}</p>
              <button
                @click="$emit('confirm')"
                data-modal-hide="danger-confirm-modal" 
                type="button" 
                class="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-semibold rounded-xl text-sm inline-flex items-center px-5 py-2.5 text-center mr-2">
                Conferma
              </button>
              <button
                data-modal-hide="danger-confirm-modal" 
                type="button" 
                class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 rounded-xl border border-gray-200 text-sm font-semibold px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-violet-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
                Annulla
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
    import { XMarkIcon } from "@heroicons/vue/24/outline";
  
    export default {
      name: 'UploadImagePopup',
      props: {
        title: String,
        description: String
      },
      emits: ['confirm'],
      components: {
          XMarkIcon
      }
    }
  </script>
  