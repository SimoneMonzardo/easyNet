<template>
  <head>
    <title>Home • easyNet</title>
  </head>

  <LoginPopup />
  <RegisterPopup />

  <div class="bg-gray-300 h-[calc(100vh-4rem)] w-screen sm:w-[calc(100vw-12rem)] lg:w-[calc(100vw-16rem)] block">
    <div class="flex flex-row">
      <button class="relative">
        <div class="block triangle"></div>
        <ChevronDoubleLeftIcon class="absolute inset-1.5 h-10 w-10 text-gray-900 rotate-45 bg-transparent" />
      </button>
    </div>

    <PostsFeedSection title="Home" :loading="loading" class="w-full h-[calc(100%-12rem)]">
      <div>
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
