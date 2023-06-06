<template>
  <NuxtLayout>
    <ErrorTemplate :error="error" />
  </NuxtLayout>
</template>
<script setup>
definePageMeta({
  layout: true,
});
const props = defineProps({
  error: Object,
});
</script>
