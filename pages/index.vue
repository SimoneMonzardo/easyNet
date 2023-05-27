<template>

  <LoginPopup/>
  <RegisterPopup/>

  <div>
    <PostsFeedSection title="Home" :loading="loading">
      <head>
        <title>Home / Easynet</title>
      </head>
      <div class="border-b">
        <PostsListFeed :posts="homePosts" />
      </div>
    </PostsFeedSection>
  </div>

</template>

<script setup>
const loading = ref(true);
const homePosts = ref([
  {
    username: "Marione",
    content: "questo è un esempio di contenuto di un post",
  },
  {
    username: "Giorgio",
    content: "questo è un altro esempio di contenuto di un post",
  },
]);

</script>