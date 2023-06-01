<template>
  <!-- Import Popup components -->
  <LoginPopup />
  <RegisterPopup />
  <ForgetPopup />

  <div class="bg-gray-300 max-h-[calc(100vh-4rem)] w-screen lg:w-[calc(100vw-16rem)] flex flex-col overflow-x-hidden">
    <div class="flex flex-row">
      <button class="relative">
        <div class="block triangle"></div>
        <ChevronDoubleLeftIcon class="absolute inset-2 h-10 w-10 text-gray-900 rotate-45 bg-transparent" />
      </button>
    </div>

    <PostsListFeed class="w-full min-h-[calc(100vh-16rem)] max-h-[calc(100vh-16rem)]" />

    <div class="flex flex-row justify-end">
      <button class="relative rotate-180">
        <div class="block triangle"></div>
        <ChevronDoubleLeftIcon class="absolute inset-2 h-10 w-10 text-gray-900 rotate-45 bg-transparent" />
      </button>
    </div>
  </div>
</template>

<script>
import { ChevronDoubleLeftIcon } from "@heroicons/vue/24/outline";

export default {
  head: {
    title: 'Home • easyNet',
    description: 'TODO: Write for SEO'
  },
  components: {
    ChevronDoubleLeftIcon
  }
}
</script>
