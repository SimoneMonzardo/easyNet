<template>
  <div class="inline-flex rounded-md w-full" role="group">
    <button
      @click="$emit('likeToggled')"
      type="button" 
      class="shadow-inner shadow-neutral-600 font-semibold gap-1 inline-flex items-center w-1/2 justify-center py-1.5 text-sm text-black bg-neutral-400 border-r border-r-gray-900 rounded-l-xl hover:bg-neutral-500 hover:text-white dark:border-white dark:text-white dark:hover:text-white dark:hover:bg-gray-700">
      <HeartIcon 
        v-if="!hasUserLike" 
        class="h-6 w-6" />
        <!-- TODO: Use the right color -->
        <!-- Filled HeartIcon. We can't use both solid and outline heroicons as components -->
      <svg 
        v-else 
        aria-hidden="true" 
        fill="red"
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
        class="h-6 w-6">
        <path d="M11.645 20.91l-.007-.003-.022-.012a15.247 15.247 0 01-.383-.218 25.18 25.18 0 01-4.244-3.17C4.688 15.36 2.25 12.174 2.25 8.25 2.25 5.322 4.714 3 7.688 3A5.5 5.5 0 0112 5.052 5.5 5.5 0 0116.313 3c2.973 0 5.437 2.322 5.437 5.25 0 3.925-2.438 7.111-4.739 9.256a25.175 25.175 0 01-4.244 3.17 15.247 15.247 0 01-.383.219l-.022.012-.007.004-.003.001a.752.752 0 01-.704 0l-.003-.001z"></path>
      </svg>
      {{ likes }}
    </button>
    <button 
      type="button" 
      class="shadow-inner shadow-neutral-600 font-semibold gap-1 inline-flex items-center w-1/2 justify-center py-1.5 text-sm text-black bg-neutral-400 border-l border-l-gray-900 rounded-r-xl hover:bg-neutral-500 hover:text-white dark:border-white dark:text-white dark:hover:text-white dark:hover:bg-gray-700">
      <ChatBubbleBottomCenterTextIcon class="h-6 w-6" />
      {{ comments }}
    </button>
  </div>
</template>

<script>
  import { HeartIcon } from "@heroicons/vue/24/outline"
  import { ChatBubbleBottomCenterTextIcon } from "@heroicons/vue/24/outline";

  export default {
    name: 'LikeCommentButtons',
    emits: ['likeToggled'],
    props: {
      likes: String,
      comments: String,
      hasUserLike: false
    },
    components: {
      HeartIcon,
      ChatBubbleBottomCenterTextIcon
    }
  }
</script>
