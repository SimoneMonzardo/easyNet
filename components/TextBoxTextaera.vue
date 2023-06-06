<template>
    
    <label for="message" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">{{ text }}</label>
    <textarea id="message" rows="4" class="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-violet-300 focus:ring-violet-600 focus:border-violet-600 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-violet-600 dark:focus:border-violet-600" placeholder="Write your thoughts here..."></textarea>

</template>

<script>
export default {
 props: ['text']
}
</script>