<template>
    <label for="large-input" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">{{ text }}</label>
    <input type="text" id="large-input" class="block w-full p-4 text-gray-900 border border-gray-300 rounded-lg bg-gray-50 sm:text-md focus:ring-violet-600 focus:border-violet-600 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-violet-600 dark:focus:border-violet-600">
</template>

<script>
export default {
 props: ['text']
}
</script>