<template>
  <div>
    <PostHeader elapsedTime="69 min" userName="utent" postName="NOME POST" profilePicture="https://upload.wikimedia.org/wikipedia/commons/8/89/%D0%A1%D0%BD%D0%B5%D0%B3_2015.jpg" profileId="prova"/>
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
