<template>
  <!-- Import Popup components -->
  <LoginPopup />
  <RegisterPopup />
  <ForgetPopup />
  <SuccessPopup />

  <div class="max-h-[calc(100vh-4rem)] w-screen grid grid-cols-[repeat(12,_minmax(0,_1fr))] grid-rows-[repeat(12,_minmax(0,_1fr))]">
    <button class="relative" @click="previousPost">
      <div class="block triangle drop-shadow-lg"></div>
      <ChevronDoubleLeftIcon class="absolute inset-2 h-10 w-10 text-violet-600 rotate-45 bg-transparent" />
    </button>

    <div v-if="pending || data.status !== 200" class="col-start-2 col-end-12 row-start-2 row-end-[12] h-full flex flex-col justify-center">
      <div role="status" class="space-y-8 animate-pulse md:space-y-0 md:space-x-8 md:flex md:items-center">
        <div class="flex items-center justify-center w-full h-96 bg-gray-300 rounded dark:bg-gray-700">
          <svg class="w-96 h-96 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor" viewBox="0 0 640 512">
            <path d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </div>
        <div class="w-full">
          <div class="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-4"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[480px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[440px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[460px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px] mb-2.5"></div>
          <div class="h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-48 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[480px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[440px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[460px] mb-2.5"></div>
          <div class="h-2 bg-gray-200 rounded-full dark:bg-gray-700 max-w-[360px]"></div>
        </div>
        <span class="sr-only">Loading...</span>
      </div>
    </div>
    <PostsFeedSection :post="data.posts[data.activePost]" v-else class="col-start-2 col-end-12 row-start-2 row-end-[12]" />

    <button class="col-start-12 col-end-[13] row-start-[12] row-end-[13] rotate-180" @click="nextPost">
      <div class="block triangle drop-shadow-lg"></div>
      <ChevronDoubleLeftIcon class="absolute inset-2 h-10 w-10 text-violet-600 rotate-45 bg-transparent" />
    </button>
  </div>

  <button hidden data-drawer-target="filters-drawer" data-drawer-show="filters-drawer" data-drawer-placement="top" aria-controls="filters-drawer"></button>

  <div 
    id="filters-drawer"
    class="absolute top-0 top-[4rem] left-0 right-0 z-10 drop-shadow-lg w-full overflow-y-auto border-b border-gray-200 rounded-b-xl transition-transform -translate-y-full bg-white dark:bg-gray-800 dark:border-gray-700"
    tabindex="-1">
    <div class="flex flex-col items-center my-5">
      <h3 class="mb-3 text-lg font-medium text-gray-900 dark:text-white">Cosa stai cercando?</h3>
      <ul class="grid w-full md:w-1/2 lg:w-1/3 gap-6 sm:grid-cols-2">
        <li class="px-10 md:px-0">
          <input type="radio" id="feed-explore" name="feed" value="explore" class="hidden peer" required :checked="!data.getFollowedPosts" @click="feedChangeRequested()">
          <label for="feed-explore" class="inline-flex items-center justify-between w-full p-4 text-gray-500 bg-white border border-violet-200 rounded-xl cursor-pointer dark:hover:text-gray-300 dark:border-gray-700 dark:peer-checked:text-violet-600 peer-checked:border-violet-600 peer-checked:text-violet-600 hover:text-gray-600 hover:bg-gray-100 dark:text-gray-400 dark:bg-gray-800 dark:hover:bg-gray-700">
            <div class="w-full text-lg font-semibold">Esplora</div>
            <GlobeEuropeAfricaIcon class="h-6 w-6 text-violet-600" />
          </label>
        </li>
        <li class="px-10 md:px-0">
          <input type="radio" id="feed-following" name="feed" value="following" class="hidden peer" :checked="data.getFollowedPosts" @click="feedChangeRequested()">
          <label for="feed-following" class="inline-flex items-center justify-between w-full p-4 text-gray-500 bg-white border border-violet-200 rounded-xl cursor-pointer dark:hover:text-gray-300 dark:border-gray-700 dark:peer-checked:text-violet-600 peer-checked:border-violet-600 peer-checked:text-violet-600 hover:text-gray-600 hover:bg-gray-100 dark:text-gray-400 dark:bg-gray-800 dark:hover:bg-gray-700">
            <div class="w-full text-lg font-semibold">Seguiti</div>
            <AtSymbolIcon class="h-6 w-6 text-violet-600" />
          </label>
        </li>
      </ul>
    </div>
    <div class="w-full sm:w-1/2 lg:w-1/3 px-10 sm:px-0 sm:mx-auto mb-5">
      <label for="search-company" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Cerca</label>
      <div class="relative">
        <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
          <MagnifyingGlassIcon class="w-5 h-5 text-gray-500 dark:text-gray-400" />
        </div>
        <input
          @input="findCompanies($event.target.value)"
          type="search"
          id="search-company"
          class="block w-full p-4 pl-10 text-sm text-gray-900 border border-gray-300 rounded-xl bg-gray-50 focus:ring-violet-600 focus:border-violet-600 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-violet-600 dark:focus:border-violet-600"
          placeholder="Trova aziende ..."
          required>
      </div>
      <ul class="min-h-[15vh] max-h-[15vh] mt-4 overflow-y-auto bg-gray-50 rounded-xl bg-opacity-50 dark:bg-gray-700">
        <li v-for="company in data.companies" v-if="!data.loadingCompanies">
          <!-- TODO: Use the right link -->
          <a class="flex items-center justify-center my-1" href="./">
            <UserCircleIcon v-if="company.profilePicture === null || company.profilePicture === ''" class="w-1/3 h-10 text-gray-900 dark:text-gray-50" />
            <img v-else class="w-1/2 lg:w-1/3 h-10" :src="company.profilePicture" />
            <span class="py-auto w-1/2 lg:w-1/3 text-gray-900 dark:text-gray-50 align-middle">{{ company.companyName }}</span>
          </a>
        </li>
        <li class="flex items-center justify-center my-1 animate-pulse" role="status" v-else>
          <svg class="w-1/2 lg:w-1/3 h-10 text-gray-200 dark:text-gray-700" aria-hidden="true" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-6-3a2 2 0 11-4 0 2 2 0 014 0zm-2 4a5 5 0 00-4.546 2.916A5.986 5.986 0 0010 16a5.986 5.986 0 004.546-2.084A5 5 0 0010 11z" clip-rule="evenodd"></path></svg>
          <div class="w-1/2 lg:w-1/3 h-2.5 bg-gray-200 rounded-full dark:bg-gray-700"></div>
        </li>
      </ul>
    </div>
    <div class="p-4 cursor-pointer hover:bg-gray-50 dark:hover:bg-gray-700" data-drawer-hide="filters-drawer">
      <span class="absolute bottom-3 w-8 h-1 -translate-x-1/2 bg-gray-300 rounded-lg left-1/2 dark:bg-gray-600"></span>
    </div>
  </div>
</template>

<script setup>
import { ChevronDoubleLeftIcon, AtSymbolIcon, GlobeEuropeAfricaIcon, MagnifyingGlassIcon, UserCircleIcon } from "@heroicons/vue/24/outline";
import useModal from '~/composables/useModal';
import useStorage from '~/composables/useStorage';

const INITIAL_POST_FETCH_COUNT = 7;
const MINIMUM_POST_TRIGGER = 5;

const initialApiRoutes = new Map();
initialApiRoutes.set(false, 'GetPostsOfRandom');
initialApiRoutes.set(true, 'GetPostsOfFollowed');

const nextApiRoutes = new Map();
nextApiRoutes.set(false, 'GetNextRandom');
nextApiRoutes.set(true, 'GetNextFollowed');

const data = reactive({
  posts: [],
  activePost: 0,
  lastFetchedPost: -1,
  status: 401,
  getFollowedPosts: false,
  companies: [],
  loadingCompanies: true
});

const savedPostsIds = [];

useHead({
  title: 'Home • Mouzone',
  meta: [{
    name:'description',
    content: 'Entra nel nostro social network professionale: connessioni globali con aziende di successo. Benvenuto!'
  }]
});

const { pending } = useFetch(`https://progettoeasynet.azurewebsites.net/Post/GetPostsOfRandom?numeroDiPost=${INITIAL_POST_FETCH_COUNT}`, {
  lazy: true,
  server: false,
  method: 'GET',
  onResponse({ response }) {
    data.status = response.status;

    const username = sessionStorage.getItem('username');

    for (const post of response._data) {
      post.hasUserLike = getPostHasUserLike(post, username);
      post.isSavedByUser = getIsPostSavedByUser(post);
      data.posts.push(post);
    }
    data.lastFetchedPost += data.posts.length;
  }
});

async function nextPost() {
  if (data.activePost < data.lastFetchedPost) {
    data.activePost++;
  }

  const token = sessionStorage.getItem('token');
  const { requireLogin } = useModal();

  if (token !== null) {
    if (data.lastFetchedPost - data.activePost < MINIMUM_POST_TRIGGER) {
      await useFetch(`https://progettoeasynet.azurewebsites.net/Post/${nextApiRoutes.get(data.getFollowedPosts)}?index=${data.lastFetchedPost}`, {
        lazy: true,
        server: false,
        method: 'GET',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        onRequest({ options }) {
          options.headers['Authorization'] = `Bearer ${token}`;
        },
        onResponse({ response }) {
          if (response.status === 200) {
            response._data.hasUserLike = getPostHasUserLike(response._data, sessionStorage.getItem('username'));
            post.isSavedByUser = getIsPostSavedByUser(post);

            data.posts.push(response._data);
            data.lastFetchedPost++;
          } else if (response.status === 401 && data.activePost >= data.lastFetchedPost) {
            const { clearSession } = useStorage();
            clearSession();
            requireLogin(false);
          }
        }
      });
    }
  } else if (data.activePost >= data.lastFetchedPost) {
    requireLogin(false);
  }
}

function previousPost() {
  if (data.activePost > 0) {
    data.activePost--;
  }
}

function getPostHasUserLike(post, username) {
  return username !== null && username !== '' && post.likes.includes(username);
}

function getIsPostSavedByUser(post) {
  return savedPostsIds.length !== 0 && savedPostsIds.includes(post.postId);
}

async function feedChangeRequested() {
  const token = sessionStorage.getItem('token');
  
  if (data.getFollowedPosts || await validateToken(token)) {
    data.status = 401;
    data.lastFetchedPost = -1;
    data.activePost = 0;
    data.posts.splice(0, data.posts.length);

    useFetch(`https://progettoeasynet.azurewebsites.net/Post/${initialApiRoutes.get(data.getFollowedPosts)}?numeroDiPost=${INITIAL_POST_FETCH_COUNT}`, {
      lazy: true,
      server: false,
      method: 'GET',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${token}`;
      },
      onResponse({ response }) {
        data.status = response.status;
        if (response.ok) {
          const username = sessionStorage.getItem('username');
          
          for (const post of response._data) {
            post.hasUserLike = getPostHasUserLike(post, username);
            post.isSavedByUser = getIsPostSavedByUser(post);
            data.posts.push(post);
          }
          data.lastFetchedPost += data.posts.length;
        }
      }
    });
  } else {
    document.getElementById('feed-explore').checked = true;

    const { requireLogin } = useModal();
    requireLogin(true);
  }
}

async function validateToken(token) {
  const { data } = await useFetch('https://progettoeasynet.azurewebsites.net/Auth/CheckToken', {
    lazy: true,
    server: false,
    method: 'GET',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Authorization': ''
    },
    onRequest({ options }) {
      options.headers['Authorization'] = `Bearer ${token}`;
    }
  });

  return data._value !== null;
}

async function findCompanies(query) {
  data.loadingCompanies = true;
  const token = sessionStorage.getItem('token');

  await useFetch(`https://progettoeasynet.azurewebsites.net/Azienda/GetNamePicture?pattern=${query}`, {
    lazy: true,
    server: false,
    method: 'GET',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Authorization': ''
    },
    onRequest({ options }) {
      options.headers['Authorization'] = `Bearer ${token}`;
    },
    onResponse({ response }) {
      if (response.ok) {
        data.companies.splice(0, data.companies.length);
        for (const company of response._data) {
          data.companies.push(company);
        }
        data.loadingCompanies = false;
      } else {
        const { requireLogin } = useModal();
        requireLogin(true);
        data.loadingCompanies = true;
      }
    }
  });
}

onMounted(() => {
  var token = sessionStorage.getItem('token');
  if (token !== null && token !== '') {
    useFetch('https://progettoeasynet.azurewebsites.net/Save/GetSavedPostsIds', {
      lazy: true,
      server: false,
      method: 'GET',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''
      },
      onRequest({ options }) {
        options.headers['Authorization'] = `Bearer ${token}`;
      },
      onResponse({ response }) {
        if (response.ok) {
          for (id in response._data) {
            savedPostsIds.append(id);
          }
        }
      }
    });
  }
});
</script>
