<template>
  <div class="flex flex-col justify-center mt-10">
    <div class="flex flex-col sm:flex-row gap-5 sm:gap-10 lg:gap-20 mx-auto">
      <div class="flex flex-col w-40 mx-auto">
        <img
          v-if="userData.data.profilepic != null"
          class="w-40 h-40 mb-3 rounded-full shadow-lg mx-auto sm:mx-0"
          :src="userData.data.profilepic"
          alt="user image"
        />
        <UserCircleIcon v-else class="w-40 h-40 text-gray-500 mb-5" />
        <ProfileHeaderButtons />
      </div>
      <div class="flex flex-col gap-y-2 my-auto w-[60vw]">
        <div class="flex flex-row sm:gap-y-4 justify-between">
          <span class="inline-block text-2xl font-semibold">
            {{ userData.data.username }}
          </span>
          <div
            class="flex flex-row gap-4 items-center justify-between align-middle"
          >
            <div v-if="!userData.data.isCurrentUser()">
              <button
                data-modal-target="popup-modal"
                data-modal-toggle="popup-modal"
                v-if="followingData.isFollowing"
                type="button"
                class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-violet-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
              >
                Following
              </button>

              <button
                type="button"
                class="text-white bg-violet-700 hover:bg-violet-800 focus:outline-none focus:ring-4 focus:ring-violet-300 font-medium rounded-full text-sm px-5 py-2.5 text-center mr-2 mb-2 dark:bg-violet-700 dark:hover:bg-violet-800 dark:focus:ring-violet-800"
                @click="follow()"
                v-else
              >
                Follow
              </button>
              <div
                id="popup-modal"
                tabindex="-1"
                class="fixed top-0 left-0 right-0 z-50 hidden p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-[calc(100%-1rem)] max-h-full"
              >
                <div class="relative w-full max-w-md max-h-full">
                  <div
                    class="relative bg-white rounded-lg shadow dark:bg-gray-700"
                  >
                    <button
                      type="button"
                      class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
                      data-modal-hide="popup-modal"
                    >
                      <svg
                        aria-hidden="true"
                        class="w-5 h-5"
                        fill="currentColor"
                        viewBox="0 0 20 20"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          fill-rule="evenodd"
                          d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                          clip-rule="evenodd"
                        ></path>
                      </svg>
                      <span class="sr-only">Close modal</span>
                    </button>
                    <div class="p-6 text-center">
                      <svg
                        aria-hidden="true"
                        class="mx-auto mb-4 text-gray-400 w-14 h-14 dark:text-gray-200"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                        ></path>
                      </svg>
                      <h3
                        class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400"
                      >
                        Are you sure you want to unfollow {{ username }} ?
                      </h3>
                      <button
                        data-modal-hide="popup-modal"
                        type="button"
                        class="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center mr-2"
                        @click="unfollow()"
                      >
                        Unfollow
                      </button>
                      <button
                        data-modal-hide="popup-modal"
                        type="button"
                        class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 rounded-lg border border-gray-200 text-sm font-medium px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600"
                      >
                        Cancel
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- <button
              type="button"
              class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-violet-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
            >
              Email
            </button> -->
            <div v-if="userData.data.isCurrentUser()">
              <div class="flex flex-col mb-2">
                <a href="/settings" type="button">
                  <Cog6ToothIcon class="h-6 w-6 text-gray-500" />
                </a>
              </div>
            </div>
            <EllipsisHorizontalIcon hidden class="h-6 w-6 text-gray-500" />
          </div>
        </div>
        <div class="flex flex-col sm:flex-row sm:gap-8">
          <h2 class="font-regular">{{ userData.data.posts }} posts</h2>

          <li>
            <button
              type="button"
              data-modal-target="followers-modal"
              data-modal-toggle="followers-modal"
              class="font-regular"
            >
              {{ userData.data.followers }} followers
            </button>
            <FollowersListPopup :followedBy="username" />
          </li>

          <li>
            <button
              type="button"
              data-modal-target="following-modal"
              data-modal-toggle="following-modal"
              class="font-regular"
            >
              {{ userData.data.following }} following
            </button>
            <FollowingListPopup :following="username" />
          </li>
        </div>
        <!-- <p>{{ full_name }}</p>
        <span class="text-sm dark:text-gray-400">{{ description }}</span> -->
      </div>
    </div>
    <div class="mt-6">
      <div class="sm:hidden">
        <label for="tabs" class="sr-only">Select your country</label>
        <select
          id="tabs"
          class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
        >
          <option>Posts</option>
          <option>Saved</option>
          <!-- <option>Mentions</option>
          <option>Liked</option> -->
        </select>
      </div>
    </div>

    <ul
      class="hidden text-sm font-medium text-center text-gray-500 divide-x divide-gray-200 rounded-lg shadow sm:flex dark:divide-gray-700 dark:text-gray-400"
    >
      <li class="w-full">
        <a
          :href="getRouteUrl('')"
          class="inline-flex justify-center w-full px-4 py-3.5 bg-white hover:text-gray-700 hover:bg-gray-50 focus:ring-4 focus:ring-blue-300 focus:outline-none dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700"
        >
          <NewspaperIcon class="h-6 w-6 text-gray-500" />
          <p class="mx-3">Posts</p></a
        >
      </li>
      <li v-if="userData.data.isCurrentUser()" class="w-full">
        <a
          :href="getRouteUrl('/saved')"
          class="inline-flex justify-center w-full px-4 py-3.5 bg-white hover:text-gray-700 hover:bg-gray-50 focus:ring-4 focus:ring-blue-300 focus:outline-none dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700"
        >
          <BookmarkSquareIcon class="h-6 w-6 text-gray-500" />
          <p class="mx-3">Saved</p></a
        >
      </li>
      <!-- <li class="w-full">
        <a
          :href="getRouteUrl('/mentions')"
          class="inline-flex justify-center w-full px-4 py-3.5 bg-white hover:text-gray-700 hover:bg-gray-50 focus:ring-4 focus:ring-blue-300 focus:outline-none dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700"
        >
          <AtSymbolIcon class="h-6 w-6 text-gray-500" />
          <p class="mx-3">Mentions</p></a
        >
      </li>
      <li class="w-full">
        <a
          :href="getRouteUrl('/liked')"
          class="inline-flex justify-center w-full px-4 py-3.5 bg-white hover:text-gray-700 hover:bg-gray-50 focus:ring-4 focus:ring-blue-300 focus:outline-none dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700"
        >
          <HeartIcon class="h-6 w-6 text-gray-500" />
          <p class="mx-3">Liked</p></a
        >
      </li> -->
    </ul>
  </div>
</template>

<script setup lang="ts">
// https://avatars.dicebear.com
import { UserCircleIcon } from "@heroicons/vue/24/outline";
import { UserData } from "../types";
import {
  NewspaperIcon,
  PaperAirplaneIcon,
  HeartIcon,
  BookMarkIcon,
  AtSymbolIcon,
  Cog6ToothIcon,
  BookmarkSquareIcon,
} from "@heroicons/vue/20/solid";
import { reactive } from "vue";

const route = useRoute();
const config = useRuntimeConfig();

const userData = reactive({
  data: new UserData(props.userBehavior, props.username),
});
const followingData = reactive({
  isFollowing: userData.data.isCurrentUserFollowing(props.userBehavior.user),
});

function getRouteUrl(route: string) {
  return `/${props.username}${route}`;
}
async function unfollow() {
  await useFetch(
    `${config.public.apiUrl}/User/Unfollow?userName=${props.username}`,
    {
      lazy: true,
      server: false,
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "",
      },

      onRequest({ request, options }) {
        options.headers["Authorization"] = `Bearer ${sessionStorage.getItem(
          "token"
        )}`;
      },
    }
  );
  await refreshNuxtData();
  userData.data = new UserData(props.userBehavior, props.username);
  followingData.isFollowing = false;
}
async function follow() {
  await useFetch(
    `${config.public.apiUrl}/User/Follow?userName=${props.username}`,
    {
      lazy: true,
      server: false,
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "",
      },
      onRequest({ request, options }) {
        options.headers["Authorization"] = `Bearer ${sessionStorage.getItem(
          "token"
        )}`;
      },
    }
  );
  await refreshNuxtData();
  userData.data = new UserData(props.userBehavior, props.username);

  followingData.isFollowing = true;
}
const props = defineProps<{
  userBehavior?: UserBehavior;
  username?: string;
}>();
</script>
