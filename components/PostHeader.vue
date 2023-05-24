<template>
  <div class="flex flex-row justify-between align-middle mx-2">
    <div class="flex flex-row gap-3">
      <img class="rounded-full h-8 w-8 my-auto" :src="profilePictureUrl" alt="profile" />
      <div class="px-1 flex flex-col">
        <a :href="profileUrl">
          <h3 class="text-xl font-semibold md:text-2xl text-gray-700">{{ userName }}</h3>
        </a>
        <small class="text-gray-400 text-xs font-md dark:bg-gray-700 dark:text-gray-400">
          {{ elapsedTime }} ago
        </small>
      </div>
    </div>
    <h2 class="my-auto text-2xl font-semibold leading-none tracking-tight text-gray-700 md:text-4xl dark:text-white">{{ postName }}</h2>
    <div>
      <button data-popover-target="actions-popover" data-popover-placement="left" data-popover-trigger="click" type="button">
        <EllipsisVerticalIcon class="h-8 w-8 text-gray-700 dark:text-white" />
      </button>
      <div data-popover id="actions-popover" role="tooltip" class="absolute z-10 invisible inline-block w-48 text-sm text-gray-500 transition-opacity duration-300 bg-white border border-gray-200 rounded-lg shadow-sm opacity-0 dark:text-gray-400 dark:border-gray-600 dark:bg-gray-800">
        <a :href="reportUrl" class="text-red-600 font-bold block w-full px-4 py-2 border-b border-gray-200 cursor-pointer hover:bg-gray-100 hover:text-red-700 focus:outline-none focus:ring-2 focus:ring-blue-700 focus:text-blue-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Report
        </a>
        <a @click="copyLink()" class="block w-full px-4 py-2 border-b border-gray-200 cursor-pointer hover:bg-gray-100 hover:text-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-700 focus:text-blue-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Copy Link
        </a>
        <a @click="openShare()" class="block w-full px-4 py-2 border-b border-gray-200 cursor-pointer hover:bg-gray-100 hover:text-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-700 focus:text-blue-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          Share
        </a>
        <a :href="profileUrl" class="block w-full px-4 py-2 rounded-b-lg cursor-pointer hover:bg-gray-100 hover:text-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-700 focus:text-blue-700 dark:border-gray-600 dark:hover:bg-gray-600 dark:hover:text-white dark:focus:ring-gray-500 dark:focus:text-white">
          About this account
        </a>
        <div data-popper-arrow></div>
      </div>
    </div>
  </div>
</template>

<script>
import { EllipsisVerticalIcon } from "@heroicons/vue/24/outline";

export default {
  props: {
    userName: String,
    postName: String,
    elapsedTime: String,
    profilePicture: String,
    profileId: String,
    postId: String
  },
  components: {
    EllipsisVerticalIcon
  },
  computed: {
    profilePictureUrl() {
      return this.profilePicture;
    },
    profileUrl() {
      // TODO: Use the rigth link
      return `./users/${this.profileId}`;
    },
    postUrl() {
      // TODO: Use the right link
      return `./posts/${this.postId}`;
    },
    reportUrl() {
      // TODO: Use the right link
      return `./posts/${this.postId}/report`;
    }
  },
  methods: {
    copyLink: function () {
      // TODO: Use the right link
      const link = `http://localhost:3000/posts/${this.postId}`;
      navigator.clipboard.writeText(link);
    },
    openShare: function() {
      // TODO: Open share modal
    }
  }
}
</script>
