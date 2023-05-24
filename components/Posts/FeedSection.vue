<template>
  <div class="border-x">
    <div class="sticky top-0 px-4 py-3 backdrop-blur-md">
      <h2 class="text-xl font-bold text-gray-800 dark:text-gray-100">
        {{ props.title }}
      </h2>
    </div>
    <!-- <div v-if="loading" class="flex items-center justify-center p-4 border-b">
      <UISpinner />
    </div>
    <div v-else>
      <slot></slot>
    </div> -->
  </div>
</template>
<script setup>
const props = defineProps({
  title: {
    type: String,
    required: true,
  },
  loading: {
    type: Boolean,
    required: true,
  },
});
</script>
