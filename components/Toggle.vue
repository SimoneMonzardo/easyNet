<template>
    <div class="text-md text-gray-900 dark:text-gray-300 mb-1 font-semibold">{{ text }}</div>
    <label class="relative inline-flex items-center cursor-pointer">
        <input type="checkbox" value="" class="sr-only peer">
        <div
            class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-violet-300 dark:peer-focus:ring-violet-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-violet-700">
        </div>
    </label>
</template>

<script>
export default {
    props: ['text'],
    name: 'Toggle'
}
</script>