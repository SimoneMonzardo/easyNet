<template>
  <section Modals>
    <!-- Upload Image Modal -->
    <UploadImagePopup @setImage="setImage" />

    <!-- Delete Image Modal -->
    <DangerConfirmModal 
      @confirm="removeImage()"
      title="Attenzione" 
      description="Sei certo di voler cancellare l'immagine?" />

    <!-- Login & Register Modals -->
    <LoginPopup />
    <RegisterPopup />
  </section>

  <div class="my-20 w-screen">
    <div class="grid gap-4 px-8 lg:px-0 lg:mx-auto w-full" :class="imageUrl === '' ? 'grid-cols-1 lg:w-1/2' : 'grid-cols-1 md:grid-cols-2 lg:w-3/4'">
      <div class="order-0 md:col-span-2 items-center flex flex-col sm:flex-row sm:justify-between">
        <!-- Title -->
        <h1 class="text-xl font-semibold tracking-tight leading-none text-gray-900 md:text-2xl lg:text-3xl dark:text-white">Crea un Post</h1>

        <!-- Images Buttons -->
        <div class="mt-5 sm:mt-0 flex flex-row" :class="imageUrl === '' ? 'justify-end' : 'w-3/4 md:w-1/2 block'">
          <button
            :class="imageUrl === '' ? 'block' : 'hidden'"
            data-modal-target="upload-image-modal"
            data-modal-show="upload-image-modal"
            type="button"
            class="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-semibold rounded-xl text-md px-3 py-1.5 mr-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800">
            Aggiungi Immagine
          </button>
          
          <div class="flex-row flex-wrap justify-center sm:flex-nowrap sm:justify-between w-full ml-2 -mr-2" :class="imageUrl === '' ? 'hidden' : 'flex'">
            <button
              data-modal-target="upload-image-modal"
              data-modal-show="upload-image-modal"
              type="button"
              class="focus:outline-none text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-semibold rounded-xl text-md px-3 py-1.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
              Modifica Immagine
            </button>
            <button 
              data-modal-target="danger-confirm-modal"
              data-modal-show="danger-confirm-modal"
              type="button"
              class="focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-semibold rounded-xl text-md px-3 py-1.5 mr-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-800">
              Elimina Immagine
            </button>
          </div>
        </div>
      </div>

      <!-- Post Text -->
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

      <!-- Post Image -->
      <div v-if="imageUrl !== ''" class="mb-0 md:mb-4 order-1 md:order-2 h-48 sm:w-full sm:h-64 lg:h-full flex items-center justify-center">        
        <img :src="imageUrl" class="w-full h-48 sm:h-64 lg:h-96 rounded-lg" />
        <input hidden type="file" name="image" id="image-input" />
      </div>

      <!-- Post Button -->
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
  head: {
    title:"Pubblica • easyNet"
  },
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
    const token = localStorage.getItem('token');
    if (token === undefined || token === null || token === '') {
      this.$router.push ('/');
    }
  },
  methods: {
    onResize() {
      if (window.innerWidth >= 1024) {
        this.rows = 15;
      } else if (window.innerWidth >= 640) {
        this.rows = 8;
      } else {
        this.rows = 3;
      }
    },
    async setImage(images) {
      const options = {};

      const uploadFileElement = document.getElementById('upload-image-modal');
      const uploadFileModal = new Modal(uploadFileElement, options);
      uploadFileModal.hide();

      const formData = new FormData();
      formData.append('file', images[0]);

      const { data } = await useFetch('https://progettoeasynet.azurewebsites.net/Post/UploadImage', {
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        method: 'POST',
        body: formData,
        onRequest({ request, options }) {
          options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
        }
      });

      if (data._value !== null) {
        this.imageUrl = data._value;
      }
    },
    removeImage() {
      this.imageUrl = '';
    }
  }
}
</script>
  