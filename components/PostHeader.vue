<template>
  <div class="flex flex-row justify-between align-middle">
    <div class="flex flex-row gap-3">
      <UserCircleIcon v-if="profilePictureUrl === undefined || profilePictureUrl === null || profilePictureUrl === ''" class="h-12 w-12 my-auto text-gray-300 dark:text-gray-600" />
      <img v-else class="rounded-full h-12 w-12 my-auto" :src="profilePictureUrl" alt="profile" />
      <div class="px-1 flex flex-col text-gray-900 dark:text-gray-50">
        <a :href="`/${username}`">
          <h3 class="text-xl font-semibold md:text-2xl hover:text-violet-600">{{ username }}</h3>
        </a>
        <small class="text-gray-400 text-xs font-md">
          {{ elapsedTime }}
        </small>
      </div>
    </div>
    <div>
      <button
        id="actions-trigger"
        @click="openPopoverMenu()"
        type="button">
        <EllipsisVerticalIcon class="h-8 w-8 text-gray-900 dark:text-white hover:text-violet-600" />
      </button>
      <div 
        data-popover 
        id="actions-popover" 
        role="tooltip"
        class="absolute z-10 invisible inline-block w-48 text-sm text-gray-500 transition-opacity duration-300 bg-white border border-gray-200 rounded-lg shadow-sm opacity-0 dark:text-gray-400 dark:border-gray-600 dark:bg-gray-800">
        <button
          @click="reportPost()"
          class="text-red-600 font-bold text-start block w-full px-4 py-2 border-b border-gray-200 cursor-pointer hover:bg-gray-100 hover:text-red-700 focus:outline-none focus:ring-2 focus:ring-violet-700 focus:text-violet-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Segnala
        </button>
        <button 
          @click="copyLink()"
          class="block text-start w-full px-4 py-2 border-b border-gray-200 cursor-pointer hover:bg-gray-100 hover:text-violet-700 focus:outline-none focus:ring-2 focus:ring-violet-700 focus:text-violet-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Copia link
        </button>
        <!-- <button 
          @click="openShare()"
          class="block text-start w-full px-4 py-2 border-b border-gray-200 cursor-pointer hover:bg-gray-100 hover:text-violet-700 focus:outline-none focus:ring-2 focus:ring-violet-700 focus:text-violet-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Condividi
        </button> -->
        <a 
          :href="`/${username}`"
          class="block w-full px-4 py-2 rounded-b-lg cursor-pointer hover:bg-gray-100 hover:text-violet-700 focus:outline-none focus:ring-2 focus:ring-violet-700 focus:text-violet-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Vai all'account
        </a>
        <div data-popper-arrow></div>
      </div>
    </div>
  </div>
</template>
  
<script>
import { EllipsisVerticalIcon } from "@heroicons/vue/24/outline";
import { UserCircleIcon } from "@heroicons/vue/20/solid";
import { Popover } from "flowbite";

export default {
  name: 'PostHeader',
  props: {
    username: String,
    elapsedTime: String,
    profilePicture: String,
    postId: 1
  },
  components: {
    EllipsisVerticalIcon,
    UserCircleIcon
  },
  computed: {
    profilePictureUrl() {
      return this.profilePicture;
    }
  },
  methods: {
    copyLink: function () {
      const link = `http://localhost:3000/${this.username}#${this.postId}`;
      navigator.clipboard.writeText(link);
    },
    openShare: function () {
      // TODO: Open share modal
    },
    async reportPost() {
      await useFetch('https://progettoeasynet.azurewebsites.net/Report/ReportPost', {
        lazy: true,
        server: false,
        method: 'POST',
        body: JSON.stringify({ postId: this.postId }),
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        onRequest({ request, options }) {
          options.headers['Authorization'] = `Bearer ${sessionStorage.getItem('token')}`;
        }
      });
    },
    openPopoverMenu() {
      const popoverElement = document.getElementById('actions-popover');
      const triggerElement = document.getElementById('actions-trigger');
      const options = {
        placement: 'left',
        triggerType: 'click'
      };

      const popover = new Popover(popoverElement, triggerElement, options);
      popover.toggle();
    }
  }
}
</script>
