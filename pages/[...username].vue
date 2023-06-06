<template>
  <div>
    <ProfileHeader />
    <UserPosts />
  </div>
  <NuxtErrorBoundary @error="someErrorLogger">
    <template #error="{ error, clearError }">
      You can display the error locally here: {{ error }}
      <button @click="clearError">This will clear the error.</button>
    </template>
  </NuxtErrorBoundary>
</template>
<script setup>
// const route = useroute();
// const { data } = await usefetch(
//   `/GetUserFromUserName/${route.params.username}`
// );
// if (!data.value) {
//   throw createerror({
//     statuscode: 404,
//     statusmessage:
//       "The link you followed may be broken, or the page may have been removed. Go back to MuzNet.",
//     fatal: true,
//   });
// }
</script>
