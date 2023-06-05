<template>
  <div class="flex justify-center my-10">
    <div class="flex flex-row gap-20">
      <div class="flex flex-col">
        <img class="w-40 h-40 mb-3 rounded-full shadow-lg"
          src="https://avatars.dicebear.com/v2/identicon/c357314a77a523661394d301674c195a.svg" alt="user image" />
        <ProfileHeaderButtons />
      </div>
      <div class="flex flex-col">
        <div class="flex flex-row gap-4 items-center">
          <span class="inline-block text-xl align-middle">
            {{ user_name }} username
          </span>
          <div class="flex gap-2 items-center justify-center align-middle">
            <button v-if="!following" type="button"
              class="text-white bg-blue-700 hover:bg-blue-800 focus:outline-none focus:ring-4 focus:ring-blue-300 font-medium rounded-full text-sm px-5 py-2.5 text-center mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
              Follow
            </button>
            <button type="button"
              class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
              v-else>
              Following
            </button>
            <button type="button"
              class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
              Email
            </button>

            <EllipsisHorizontalIcon class="h-6 w-6 text-gray-500" />
          </div>
        </div>
        <div class="flex flex-row gap-8">
          <h2 class="font-regular">{{ post_number }}0 posts</h2>




          <li>
            <button type="button" data-modal-target="followers-modal" data-modal-toggle="followers-modal"
              class="font-regular">{{
                followers_count }}0 followers</button>
            <FollowersListPopup followedBy="CR" />
          </li>



          <li>
            <button type="button" data-modal-target="following-modal" data-modal-toggle="following-modal"
              class="font-regular">0 following</button>
            <FollowingListPopup following="CR" />
          </li>
        </div>
        <p>{{ full_name }}</p>
        <span class="text-sm dark:text-gray-400">{{ description }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { EllipsisHorizontalIcon } from "@heroicons/vue/20/solid";

export default {
  props: {
    user_name: String,
    full_name: String,
    profile_picture: String,
    followers_count: String,
    following_count: String,
    post_number: String,
    description: String,
    haveBanner: Boolean,
    myProfile: Boolean,
    following: Boolean,
  },
  computed: {
    image() {
      return this.profile_picture;
    },
  },
};
</script>
