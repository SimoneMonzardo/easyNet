<template>
  <div id="upload-image-modal" tabindex="-1" aria-hidden="true"
    class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
    <div class="relative w-full max-w-lg max-h-full">
      <div class="relative bg-white rounded-lg shadow dark:bg-gray-700">
        <button type="button"
          class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
          data-modal-hide="upload-image-modal">
          <XMarkIcon class="h-6 w-6 text-gray-500 dark:text-gray-400" />
          <span class="sr-only">Chiudi</span>
        </button>
        <div class="px-6 py-6 lg:px-8">
          <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">Scegli un'immagine:</h3>
          <div class="space-y-6">

            <div class="flex items-center justify-center w-full">
              <label for="dropzone-file"
                class="flex flex-col items-center justify-center w-full h-64 border-2 border-gray-300 border-dashed rounded-lg cursor-pointer bg-gray-50 dark:hover:bg-bray-800 dark:bg-gray-700 hover:bg-gray-100 dark:border-gray-600 dark:hover:border-gray-500 dark:hover:bg-gray-600">
                <div class="flex flex-col items-center justify-center pt-5 pb-6">
                  <svg aria-hidden="true" class="w-10 h-10 mb-3 text-gray-400" fill="none" stroke="currentColor"
                    viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                      d="M7 16a4 4 0 01-.88-7.903A5 5 0 1115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12"></path>
                  </svg>
                  <p class="mb-2 text-sm text-gray-500 dark:text-gray-400"><span class="font-semibold">Clicca per
                      caricare</span> oppure trascina e rilascia</p>
                  <p class="text-xs text-gray-500 dark:text-gray-400">SVG, PNG, JPG o GIF</p>
                </div>
                <input 
                  id="dropzone-file" 
                  type="file" 
                  class="hidden" 
                  @change="$emit('setImage', $event.target.files)"/>
              </label>
            </div>

            <div class="flex flex-row justify-between">
              <button data-modal-hide="upload-image-modal" type="button"
                class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 rounded-xl border border-gray-200 text-sm font-semibold px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
                Annulla
              </button>
              <button
                v-if="showDelete"
                type="button" 
                data-modal-hide="danger-confirm-modal" 
                class="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-semibold rounded-xl text-sm inline-flex items-center px-5 py-2.5 text-center"
                @click="$emit('delete')">
                Elimina
              </button>
            </div>
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
    showDelete: false
  },
  emits: ['setImage', 'delete'],
  components: {
    XMarkIcon
  }
}
</script>
