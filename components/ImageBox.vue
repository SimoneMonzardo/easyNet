<template>
    <img class="h-auto max-w-lg rounded-lg" :src="imageUrl" alt="image">
</template>

<script>
export default {
    props: {
        text: {
        type: String,
        required: true
        }
    },
    computed: {
        imageUrl() {
        return this.text;
        }
    }
}
</script>
  