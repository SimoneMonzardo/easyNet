<template>
  <div class="flex flex-row gap-3">
    <div class="flex flex-col">
      <img
        class="w-40 h-40 mb-3 rounded-full shadow-lg"
        :src="image"
        alt="user image"
      />
      <ProfileHeaderButtons />
    </div>
    <div class="flex flex-col">
      <div class="flex flex-row gap-3 align-middle">
        <h2 class="font-regular">{{ user_name }}asdsa</h2>
        <div class="flex flex-row gap-2">
          <button
            type="button"
            class="text-white bg-blue-700 hover:bg-blue-800 focus:outline-none focus:ring-4 focus:ring-blue-300 font-medium rounded-full text-sm px-5 py-2.5 text-center mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            Default
          </button>
          <button
            type="button"
            class="py-2.5 px-5 mr-2 mb-2 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-full border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
          >
            Alternative
          </button>
        </div>
      </div>
      <div class="flex flex-row gap-3">
        <h2 class="font-regular">{{ followers_count }} posts</h2>
        <h2 class="font-regular">{{ followers_count }} followers</h2>
        <h2 class="font-regular">{{ following_count }} following</h2>
      </div>
      <span class="text-sm dark:text-gray-400">{{ description }}</span>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    user_name: String,
    profile_picture: String,
    followers_count: String,
    following_count: String,
    description: String,
    haveBanner: Boolean,
  },
  computed: {
    image() {
      return this.profile_picture;
    },
  },
};
</script>
