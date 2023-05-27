<template>
  <head>
    <title>Home • easyNet</title>
  </head>

  <LoginPopup />
  <RegisterPopup />

  <div class="bg-gray-300 h-[calc(100vh-64px)] w-screen sm:w-[calc(100vw-16rem)] block">
    <button class="relative">
      <div class="block triangle"></div>
      <ChevronDoubleLeftIcon class="absolute inset-1.5 h-10 w-10 text-gray-900 rotate-45 bg-transparent" />
    </button>

    <PostsFeedSection title="Home" :loading="loading" class="w-full h-[calc(100%-180px)]">
      <div class="border-b">
        <PostsListFeed :posts="homePosts" />
      </div>
    </PostsFeedSection>

    <div class="flex flex-row justify-end">
      <button class="relative rotate-180">
        <div class="block triangle"></div>
        <ChevronDoubleLeftIcon class="absolute inset-1.5 h-10 w-10 text-gray-900 rotate-45 bg-transparent" />
      </button>
    </div>
  </div>
</template>

<script setup>
const loading = ref(true);
const homePosts = ref([
  {
    username: "Marione",
    content: "questo è un esempio di contenuto di un post",
  },
  {
    username: "Giorgio",
    content: "questo è un altro esempio di contenuto di un post",
  },
]);
</script>

<script>
import { ChevronDoubleLeftIcon } from "@heroicons/vue/24/outline";

export default {
  components: {
    ChevronDoubleLeftIcon
  }
}
</script>
