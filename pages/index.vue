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

<button data-modal-target="authentication-modal" data-modal-toggle="authentication-modal" class="block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
  login modal
</button>
<button data-modal-target="register-modal" data-modal-toggle="register-modal" class="block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
  register modal
</button>
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