<template>
  <div class="grid grid-cols-2 md:grid-cols-3 gap-4">
    <div v-for="(post, index) in userPosts">
      <p>{{ post.content }}</p>
      <!-- <img
        class="h-auto max-w-full rounded-lg"
        src="https://flowbite.s3.amazonaws.com/docs/gallery/square/image-11.jpg"
        alt=""
      /> -->
    </div>
  </div>
</template>
<script setup lang="ts">
const props = defineProps<{
  userPosts?: Post[];
}>();
</script>
