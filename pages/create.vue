<!-- TODO: Use markdown editor -->
<!-- TODO: Implement formatting buttons -->
<!-- TODO: Add Image Modal (Reuse the component) -->

<template>
  <section Modals>
    <LoginPopup />
    <RegisterPopup />
  </section>

  <div class="my-auto w-screen">
    <div class="grid grid-cols-1 gap-4 px-8 lg:px-0 lg:mx-auto w-full md:grid-cols-2 lg:w-3/4">
      <div class="order-0 md:col-span-2 items-center flex flex-col sm:flex-row sm:justify-between">
        <h1 class="text-xl font-semibold tracking-tight leading-none text-gray-900 md:text-2xl lg:text-3xl dark:text-white">Crea un Post</h1>
        <div class="mt-5 sm:mt-0 flex flex-row justify-end">
          <button 
            v-if="imageUrl === ''" 
            type="button"
            class="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-semibold rounded-xl text-md px-3 py-1.5 mr-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800">
            Aggiungi Immagine
          </button>
          <div v-else class="flex flex-row flex-wrap justify-center">
            <button
              type="button"
              class="focus:outline-none text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-semibold rounded-xl text-md px-3 py-1.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
              Modifica Immagine
            </button>
            <button 
              @click="imageUrl = ''" 
              type="button"
              class="focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-semibold rounded-xl text-md px-3 py-1.5 mr-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-800">
              Elimina Immagine
            </button>
          </div>
        </div>
      </div>
      <div class="order-2 md:order-1 w-full mb-4 border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-600 h-48 sm:h-64 lg:h-full">
        <div class="flex bg-gray-50 items-center justify-between px-3 py-2 border-b dark:border-gray-600 dark:bg-gray-700">
          <div class="flex flex-wrap items-center divide-gray-200 sm:divide-x dark:divide-gray-600">
            <div class="flex items-center space-x-1 sm:pr-4">
              <button type="button"
                class="p-2 text-gray-500 rounded cursor-pointer hover:text-gray-900 hover:bg-gray-100 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600">
                <CodeBracketIcon class="h-5 w-5 text-gray-500 dark:text-gray-400" />
                <span class="sr-only">Cambia formattazione</span>
              </button>
              <button type="button"
                class="p-2 text-gray-500 rounded cursor-pointer hover:text-gray-900 hover:bg-gray-100 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600">
                <FaceSmileIcon class="h-5 w-5 text-gray-500 dark:text-gray-400" />
                <span class="sr-only">Aggiungi emoji</span>
              </button>
            </div>
            <div class="flex flex-wrap items-center space-x-1 sm:pl-4">
              <button type="button"
                class="p-2 text-gray-500 rounded cursor-pointer hover:text-gray-900 hover:bg-gray-100 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600">
                <ListBulletIcon class="h-5 w-5 text-gray-500 dark:text-gray-400" />
                <span class="sr-only">Aggiungi lista</span>
              </button>
            </div>
          </div>
        </div>
        <div class="px-4 py-2 bg-white rounded-b-lg dark:bg-gray-800">
          <textarea id="editor" :rows="rows"
            class="block w-full px-0 text-sm text-gray-800 bg-white border-0 dark:bg-gray-800 focus:ring-0 dark:text-white dark:placeholder-gray-400"
            placeholder="Scrivi il testo qui..." v-model="postText" required></textarea>
        </div>
      </div>
      <div class="mb-0 md:mb-4 order-1 md:order-2 h-48 sm:w-full h-64 lg:h-full">
        <div v-if="imageUrl === ''"
          class="flex items-center justify-center w-full h-full bg-gray-200 rounded-lg dark:bg-gray-700">
          <svg class="w-12 h-12 text-gray-100" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor"
            viewBox="0 0 640 512">
            <path
              d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </div>

        <img v-else :src="imageUrl" class="w-12 w-12" />
      </div>
      <div class="px-4 md:px-40 md:col-span-2 order-3">
        <button 
          type="button"
          :disabled="postText === ''"
          class="w-full focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-semibold rounded-xl text-md px-5 py-2.5 mr-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800 disabled:bg-green-300 disabled:cursor-not-allowed disabled:dark:bg-green-400">
          Pubblica
        </button>
      </div>
    </div>
  </div>
</template>
  
<script>
import { CodeBracketIcon } from "@heroicons/vue/24/outline"
import { ListBulletIcon } from "@heroicons/vue/24/outline";
import { FaceSmileIcon } from "@heroicons/vue/24/outline";

export default {
  data: () => ({
    postText: '',
    imageUrl: '',
    rows: 14
  }),
  components: {
    CodeBracketIcon,
    ListBulletIcon,
    FaceSmileIcon
  },
  mounted: function () {
    this?.$nextTick(function () {
      this.onResize();
    });
    window.addEventListener('resize', this.onResize);
  },
  methods: {
    onResize() {
      if (window.innerWidth >= 1024) {
        this.rows = 14;
      } else if (window.innerWidth >= 640) {
        this.rows = 8;
      } else {
        this.rows = 3;
      }
    }
  }
}
</script>
  