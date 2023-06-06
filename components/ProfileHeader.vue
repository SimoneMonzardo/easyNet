<template>
  <div class="flex justify-center my-10">
    <div class="flex flex-row gap-20">
      <div class="flex flex-col">
        <img
          class="w-40 h-40 mb-3 rounded-full shadow-lg"
          src="https://avatars.dicebear.com/v2/identicon/c357314a77a523661394d301674c195a.svg"
          alt="user image"
        />
        <ProfileHeaderButtons />
      </div>
      <div class="flex flex-col">
        <div class="flex flex-row gap-4 items-center">
          <span class="inline-block text-xl align-middle">
            {{ userData.username }}
          </span>
          <div
            class="flex flex-row gap-4 items-center justify-between align-middle"
          >
            <div v-if="!userData.isCurrentUser()">
              <button
                v-if="userData.isCurrentUserFollowing(userBehavior.user)"
                type="button"
                class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-violet-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
              >
                Following
              </button>
              <button
                type="button"
                class="text-white bg-violet-700 hover:bg-violet-800 focus:outline-none focus:ring-4 focus:ring-violet-300 font-medium rounded-full text-sm px-5 py-2.5 text-center mr-2 mb-2 dark:bg-violet-700 dark:hover:bg-violet-800 dark:focus:ring-violet-800"
                v-else
              >
                Follow
              </button>
            </div>
            <!-- <button
              type="button"
              class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-violet-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
            >
              Email
            </button> -->
            <div v-if="userData.isCurrentUser()">
              <div class="flex flex-col mb-2">
                <a href="/settings" type="button">
                  <Cog6ToothIcon class="h-6 w-6 text-gray-500" />
                </a>
              </div>
            </div>
            <EllipsisHorizontalIcon class="h-6 w-6 text-gray-500" />
          </div>
        </div>
        <div class="flex flex-row gap-8">
          <h2 class="font-regular">{{ userData.posts }} posts</h2>

          <li>
            <button
              type="button"
              data-modal-target="followers-modal"
              data-modal-toggle="followers-modal"
              class="font-regular"
            >
              {{ userData.followers }} followers
            </button>
            <FollowersListPopup :followedBy="'filippoferrario'" />
          </li>

          <li>
            <button
              type="button"
              data-modal-target="following-modal"
              data-modal-toggle="following-modal"
              class="font-regular"
            >
              {{ userData.following }} following
            </button>
            <FollowingListPopup :following="'filippoferrario'" />
          </li>
        </div>
        <!-- <p>{{ full_name }}</p>
        <span class="text-sm dark:text-gray-400">{{ description }}</span> -->
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// https://avatars.dicebear.com
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

const props = defineProps<{
  userBehavior?: UserBehavior;
  username?: string;
}>();

const userData = new UserData(props.userBehavior, props.username);

function getRouteUrl(route: string) {
  return `/${props.username}${route}`;
}
</script>
