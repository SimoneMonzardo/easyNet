<template>
  <!-- Import Popup components -->
  <LoginPopup />
  <RegisterPopup />
  <ForgetPopup />
  <SuccessPopup />

  <div class="max-h-[calc(100vh-4rem)] w-screen grid grid-cols-[repeat(12,_minmax(0,_1fr))] grid-rows-[repeat(12,_minmax(0,_1fr))]">
    <button class="relative" @click="previousPost">
      <div class="block triangle"></div>
      <ChevronDoubleLeftIcon class="absolute inset-2 h-10 w-10 text-blue-500 rotate-45 bg-transparent" />
    </button>

    <div v-if="pending || data.status !== 200" class="col-start-2 col-end-12 row-start-2 row-end-[12] flex-col justify-center">
      <div role="status" class="space-y-8 animate-pulse md:space-y-0 md:space-x-8 md:flex md:items-center">
        <div class="flex items-center justify-center w-full h-96 bg-gray-300 rounded dark:bg-gray-700">
          <svg class="w-96 h-96 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor" viewBox="0 0 640 512"><path d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z"/></svg>
        </div>
        <div class="w-full">
          <div class="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-4"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[480px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[440px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[460px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px]"></div>
        </div>
        <span class="sr-only">Loading...</span>
      </div>
    </div>
    <PostsFeedSection :post="data.posts[data.activePost]" v-else class="col-start-2 col-end-12 row-start-2 row-end-[12]"/>

    <button class="col-start-12 col-end-[13] row-start-[12] row-end-[13] rotate-180" @click="nextPost">
      <div class="block triangle"></div>
      <ChevronDoubleLeftIcon class="absolute inset-2 h-10 w-10 text-blue-500 rotate-45 bg-transparent" />
    </button>
  </div>
</template>

<script>
import { ChevronDoubleLeftIcon } from "@heroicons/vue/24/outline";

export default {
  head: {
    title: 'Home • easyNet',
  },
  components: {
    ChevronDoubleLeftIcon
  }
}
</script>

<script setup>
const INITIAL_POST_FETCH_COUNT = 7;
const MINIMUM_POST_TRIGGER = 5;

const nextApiRoutes = new Map();
nextApiRoutes.set(false, 'Post/GetNextRandom');
nextApiRoutes.set(true, 'Post/GetNextFollowed');

const data = reactive({ 
  posts: [],
  activePost: 0,
  lastFetchedPost: -1,
  status: 401,
  getFollowedPosts: false
});

const { pending } = useFetch(`https://progettoeasynet.azurewebsites.net/Post/GetPostsOfRandom?numeroDiPost=${INITIAL_POST_FETCH_COUNT}`, {
  lazy: true,
  server: false,
  method: 'GET',
  onResponse({ response }) {
    data.status = response.status;

    const username = localStorage.getItem('username');

    for (const post of response._data) {
      post.hasUserLike = getPostHasUserLike(post, username);
      data.posts.push(post);    
    }
    data.lastFetchedPost += data.posts.length;
  }
});

async function nextPost() {
  if (data.activePost < data.lastFetchedPost) {
    data.activePost++;
  }

  const token = localStorage.getItem('token');

  if (token !== null) {
    if (data.lastFetchedPost - data.activePost < MINIMUM_POST_TRIGGER) {
      await useFetch(`https://progettoeasynet.azurewebsites.net/${nextApiRoutes.get(data.getFollowedPosts)}?index=${data.lastFetchedPost}`, {
        lazy: true,
        server: false,
        method: 'GET',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        onRequest({ options }){
          options.headers['Authorization'] = `Bearer ${token}`;
        },
        onResponse({ response }) {
          if (response.status === 200) {
            response._data.hasUserLike = getPostHasUserLike(response._data, localStorage.getItem('username'));

            data.posts.push(response._data);
            data.lastFetchedPost++;
          } else if (response.status === 401 && data.activePost >= data.lastFetchedPost) {
            // TODO: Logout (Clear localstorage). Create a common method to do that
            requireLogin();
          }
        }
      });
    }
  } else if (data.activePost >= data.lastFetchedPost) {
    requireLogin();
  }
}

function previousPost() {
  if (data.activePost > 0) {
    data.activePost--;
  }
}

function requireLogin() {
  const options = {
    closable: false
  };

  const loginElement = document.getElementById('authentication-modal');
  document.getElementById('close-login-modal-button').classList.add('hidden');
  const loginModal = new Modal(loginElement, options);
  loginModal.show();
}

function getPostHasUserLike(post, username) {
  return post.likes.includes(username);
}
</script>
