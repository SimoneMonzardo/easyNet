<template>
  <div class="container mx-auto justify-center align-middle">
    <section>
      <div class="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
        <div class="mx-auto max-w-screen-sm text-center">
          <h1 class="mb-4 text-7xl tracking-tight font-extrabold lg:text-9xl text-gray-600 dark:text-white">
            {{ error.statusCode }}
          </h1>
          <p class="mb-4 text-3xl tracking-tight font-semibold text-gray-600 md:text-4xl dark:text-white">
            {{ error.statusMessage }}
          </p>
        </div>
      </div>
    </section>
  </div>
</template>
<script setup lang="ts">
const props = defineProps<{
  error?: any;
}>();
</script>
