<template>
  <NuxtErrorBoundary @error="someErrorLogger">
    <template #error="{ error, clearError }">
      You can display the error locally here: {{ error }}
      <button @click="clearError">This will clear the error.</button>
    </template>
    <div>
      <ProfileHeader />
      <p class="text-black dark:text-white">@{{ $route.params.username }}</p>
    </div>
  </NuxtErrorBoundary>
</template>
<script setup>
const route = useRoute();
// const { data } = await useFetch(`/api/users/${route.params.username}`);
throw createError({ statusCode: 404, statusMessage: "Page Not Found" });
</script>
