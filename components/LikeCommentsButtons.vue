<template>
  <div class="flex flex-row">
    <button class="shadow-inner shadow-neutral-600 rounded-l-full bg-neutral-400 flex flex-row gap-x-2 pl-4 pr-2 py-1 font-semibold align-middle">
      <HeartIcon class="h-6 w-6 text-black" />
      {{ likes }}
    </button>
    <button class="border-l-2 border-l-neutral-700 shadow-inner shadow-neutral-600 rounded-r-full bg-neutral-400 flex flex-row gap-x-2 pl-2 pr-4 py-1 font-semibold">
      <ChatBubbleBottomCenterTextIcon class="h-6 w-6 text-black" />
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