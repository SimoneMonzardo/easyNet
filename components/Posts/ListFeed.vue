<template>
    <div>
        <h1>listFeed</h1>

        <ul>
            <li v-for="post in props.posts">
                {{  post }}
            </li>
        </ul>


    </div>
</template>
<script setup>
const props = defineProps({
    posts: {
        type: Array,
        required: true
    }
})
</script>