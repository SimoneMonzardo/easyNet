<template>
  <div>
      <div v-if="pending" class="my-auto">
        
      </div>
      <div class="w-full px-2 sm:px-8 md:px-12 xl:w-5/6 xl:px-0 2xl:w-3/4 mx-auto h-[calc(100vh-16rem)] flex flex-col justify-center" v-else>
        <PostsFeedSection :post="data.posts[data.activePost]" />
      </div>
  </div>
</template>

<script setup>
const INITIAL_POST_FETCH_COUNT = 7;
const MINIMUM_POST_TRIGGER = 5;

const data = reactive({ 
    posts: [],
    activePost: 0,
    nextFetchPost: 1
});

// const { pending } = await useFetch(`https://progettoeasynet.azurewebsites.net/Post/GetPostsOfRandom?numeroDiPost=${INITIAL_POST_FETCH_COUNT}`, {
//   lazy: true,
//   server: false,
//   method: 'GET',
//   onResponse({ response }) {
//     console.log(response);
//     for (const post of response._data) {
//         data.posts.push(post);    
//     }
//     nextPost += data.posts.length;
//   }
// });
const { pending } = await useFetch(`https://progettoeasynet.azurewebsites.net/Post/GetThreeRandomPostForNotauth`, {
  lazy: true,
  server: false,
  method: 'GET',
  onResponse({ response }) {
    console.log(response);
    for (const post of response._data) {
        data.posts.push(post);    
    }
  }
});

async function nextPost() {
  data.activePost++;

  if (data.nextFetchPost - data.activePost < MINIMUM_POST_TRIGGER) {
    await useFetch(`https://progettoeasynet.azurewebsites.net/Post/GetNextRandom?index=${data.nextPost}`, {
      lazy: true,
      server: false,
      method: 'GET',
      onResponse({ response }) {
        data.posts.push(response._data);
        data.nextFetchPost++;
      }
    });
  }
}

function previousPost() {
  data.activePost--;
}
</script>
