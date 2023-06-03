<template>
  <div
    class="grid sm:grid-cols-2 lg:grid-cols-4 grid-rows-[repeat(12,_minmax(0,_1fr))] w-full h-full gap-x-1 sm:gap-x-2 md:gap-x-3 gap-y-3">
    <PostHeader :username="post.username" :elapsedTime="elapsed" class="lg:col-span-3" />

    <div
      class="row-start-2 row-end-[8] sm:row-end-[9] lg:row-end-[13] sm:col-span-2 lg:col-span-3 h-full flex flex-col justify-center rounded-xl gap-1 sm:gap-2 md:gap-3 p-6 bg-white border border-gray-200 shadow-xl dark:bg-gray-800 dark:border-gray-700">
      <div v-html="content.content" class="mx-auto text-gray-900 dark:text-gray-50"
        :class="content.data.image === '' ? 'h-full' : 'h-9'"></div>
      <img v-if="content.data.image !== ''" :src="content.data.image" class="h-auto max-h-full rounded-lg mx-auto" />
    </div>

    <LikeCommentsButtons
      class="row-start-[8] sm:row-start-1 sm:col-start-2 lg:col-start-4 w-full h-10" 
      :likes="likes"
      :comments="comments" 
      :hasUserLike="post.hasUserLike"
      :isSavedByUser="post.isSavedByUser"
      @likeToggled="toggleLike()"
      @saveToggled="toggleSave()" />

    <div
      class="row-start-[9] col-span-2 lg:col-span-1 lg:col-start-4 lg:row-start-2 row-end-[13] shadow-inner shadow-gray-400 dark:shadow-gray-800 rounded-xl bg-gray-100 dark:bg-gray-600 flex flex-col justify-between h-full max-h-full">
      <ul class="tracking-tight text-gray-900 dark:text-gray-50 overflow-y-scroll max-h-full my-4">
        <li v-for="comment in post.comments" class="w-full px-4 py-2">
          <h6 class="text-md font-semibold">{{ comment.username }}</h6>
          <p class="text-xs leading-tight">{{ comment.content }}</p>
        </li>
      </ul>
      <div class="w-full">
        <label for="addAComment" class="sr-only">Commenta...</label>
        <div class="flex items-center px-2 bg-gray-300 bg-opacity-80 rounded-xl dark:bg-gray-700">
          <textarea v-model="additionalData.userComment" id="addAComment" rows="1"
            class="block py-1 px-1.5 w-full text-sm text-gray-900 bg-white rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 dark:text-gray-50 dark:focus:ring-blue-500 dark:focus:border-blue-500"
            placeholder="Commenta..." style="resize: none"></textarea>
          <button @click="postComment()" type="submit"
            class="inline-flex justify-center p-2 rounded-full cursor-pointer text-blue-500 dark:text-blue-800">
            <PaperAirplaneIcon class="w-6 h-6" />
            <span class="sr-only">Invia</span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { PaperAirplaneIcon } from "@heroicons/vue/24/outline";
import { reactive } from 'vue';
import * as matter from 'gray-matter';

const suffixes = ['', 'K', 'M'];

const props = defineProps({
  post: { username: '' }
});

const additionalData = reactive({
  userComment: '',
});

const content = computed(() => {
  const data = matter(`---\nimage: https://media-assets.wired.it/photos/615f1a10cae11de32015125c/16:9/w_1280,c_limit/1486735809_Colosseo.jpg\n---\n<h1>${props.post.content}</h1>`);
  return data;
  //return props.post.content;
})

const likes = computed(() => {
  var suffixIndex = 0;
  var likesCount = props.post.likes.length;
  while (likesCount > 999) {
    likesCount = Math - floor(likesCount / 1000);
    suffixIndex++;
  }

  return likesCount + suffixes[suffixIndex];
});

const comments = computed(() => {
  var suffixIndex = 0;
  var commentsCount = props.post.comments.length;
  while (commentsCount > 999) {
    commentsCount = Math - floor(commentsCount / 1000);
    suffixIndex++;
  }

  return commentsCount + suffixes[suffixIndex];
});

const elapsed = computed(() => {
  if (props.post.dataDiCreazione === null) {
    return "Ora";
  }

  const postDate = new Date(props.post.dataDiCreazione);
  const currentDate = new Date();

  // TODO: Calcolare meglio le date e sistemare i singolari
  const yearDiff = currentDate.getFullYear() - postDate.getFullYear();
  if (yearDiff > 0) {
    return `${yearDiff} anni fa`;
  }

  const monthDiff = currentDate.getMonth() - postDate.getMonth();
  if (monthDiff > 0) {
    return `${monthDiff} mesi fa`;
  }

  const daysDiff = currentDate.getDate() - postDate.getDate();
  if (daysDiff > 0) {
    return `${daysDiff} giorni fa`;
  }

  return 'Oggi';
});

async function postComment() {
  const comment = {
    postId: props.post.postId,
    commentId: 0,
    content: additionalData.userComment
  };

  await useFetch('https://progettoeasynet.azurewebsites.net/Comments/UpsertComment', {
    lazy: true,
    server: false,
    method: 'POST',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Authorization': ''
    },
    body: JSON.stringify(comment),
    onRequest({ options }) {
      options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
    },
    onResponse({ response }) {
      additionalData.userComment = '';

      if (response.ok) {
        props.post.comments.push({
          username: localStorage.getItem('username'),
          content: comment.content
        })
      } else {
        // TODO: Show login modal, create a common method to reuse
      }
    }
  });
}
async function toggleLike() {
  const token = localStorage.getItem('token');
  if (token === null || token === '') {
    // TODO: Show Login Modal
    return;
  }

  props.post.hasUserLike = !props.post.hasUserLike;
  if (props.post.hasUserLike) {
    props.post.likes.push({});
  } else {
    props.post.likes.pop();
  }

  await useFetch(`https://progettoeasynet.azurewebsites.net/Like/PostLike?postId=${props.post.postId}`, {
    lazy: true,
    server: false,
    method: 'POST',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Authorization': ''
    },
    onRequest({ options }) {
      options.headers['Authorization'] = `Bearer ${token}`;
    },
    onResponse({ response }) {
      if (!response.ok) {
        props.post.hasUserLike = !props.post.hasUserLike;
        if (props.post.hasUserLike) {
          props.post.likes.push({});
        } else {
          props.post.likes.pop();
        }
      }
    }
  });
}
async function toggleSave() {
  const token = localStorage.getItem('token');
  if (token === null || token === '') {
    // TODO: Show Login Modal
    return;
  }

  props.post.isSavedByUser = !props.post.isSavedByUser;
  
  await useFetch(`https://progettoeasynet.azurewebsites.net/Save/PostSave?postId=${props.post.postId}`, {
    lazy: true,
    server: false,
    method: 'POST',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Authorization': ''
    },
    onRequest({ options }) {
      options.headers['Authorization'] = `Bearer ${token}`;
    },
    onResponse({ response }) {
      if (!response.ok) {
        props.post.isSavedByUser = !props.post.isSavedByUser;
      }
    }
  });
}
</script>
