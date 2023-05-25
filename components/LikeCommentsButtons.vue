<template>
  <div class="inline-flex rounded-md" role="group">
    <button type="button" class="shadow-inner shadow-neutral-600 font-semibold gap-1 inline-flex items-center px-3 py-1 text-sm font-medium text-black bg-neutral-400 border-r border-r-gray-900 rounded-l-full hover:bg-neutral-500 hover:text-white focus:z-10 focus:ring-2 focus:ring-gray-500 focus:bg-gray-900 focus:text-white dark:border-white dark:text-white dark:hover:text-white dark:hover:bg-gray-700 dark:focus:bg-gray-700">
      <HeartIcon class="h-6 w-6" />
      {{ likes }}
    </button>
    <button type="button" class="shadow-inner shadow-neutral-600 font-semibold gap-1 inline-flex items-center px-3 py-1 text-sm font-medium text-black bg-neutral-400 border-l border-l-gray-900 rounded-r-full hover:bg-neutral-500 hover:text-white focus:z-10 focus:ring-2 focus:ring-gray-500 focus:bg-gray-900 focus:text-white dark:border-white dark:text-white dark:hover:text-white dark:hover:bg-gray-700 dark:focus:bg-gray-700">
      <ChatBubbleBottomCenterTextIcon class="h-6 w-6" />
      {{ comments }}
    </button>
  </div>
</template>

<script>
  import { HeartIcon } from "@heroicons/vue/24/outline"
  import { ChatBubbleBottomCenterTextIcon } from "@heroicons/vue/24/outline";
  export default {
    props: {
      likes: String,
      comments: String
    },
    components: {
        HeartIcon,
        ChatBubbleBottomCenterTextIcon
    }
  }
</script>
