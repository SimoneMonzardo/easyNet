<template>
  <div class="flex flex-row gap-x-2">
    <a v-if="anonymus" href="./Register" class="bg-neutral-400 hover:bg-neutral-500 focus:ring-4 font-semibold focus:ring-neutral-300 font-medium rounded-full text-sm px-5 py-2 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Register</a>
    <a v-if="anonymus" href="./Login" class="bg-neutral-400 hover:bg-neutral-500 focus:ring-4 font-semibold focus:ring-neutral-300 font-medium rounded-full text-sm px-5 py-2 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Log in</a>
  </div>
</template>

<script>
  export default {
    props: ['anonymus']
  }
</script>