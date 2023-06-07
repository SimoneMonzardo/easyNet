<template>
  <NuxtLayout name="user-page">
    <!-- Declare items with modals' ids to avoid errors -->
    <div hidden id="authentication-modal"></div>
    <div hidden id="register-modal"></div>
    <div hidden id="forget-modal"></div>
    <div hidden id="success-modal"></div>
    <div hidden id="filters-drawer"></div>
    <button hidden data-modal-target="highlight-modal"></button>

    <div id="highlight-modal" tabindex="-1"
      class="bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
      <div class="relative w-full max-w-4xl max-h-full">
        <!-- Modal content -->
        <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 max-h-[80vh]">
          <!-- Modal header -->
          <div class="flex items-center justify-between p-6 pb-0 rounded-t dark:border-gray-600">
            <PostHeader class="w-full mr-1 sm:mr-5" :username="data.selectedPost.username"
              :elapsedTime="data.selectedPost.elapsed" :profilePicture="data.selectedPost.imageUrl"
              :postId="data.selectedPost.postId" />
            <button type="button"
              class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 -mt-3 ml-auto inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white"
              data-modal-hide="highlight-modal">
              <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20"
                xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd"
                  d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                  clip-rule="evenodd"></path>
              </svg>
              <span class="sr-only">Chiudi</span>
            </button>
          </div>
          <!-- Modal body -->
          <div class="p-6 pt-3 space-y-3 w-full max-h-full">
            <div
              class="h-full flex flex-col justify-center rounded-xl gap-1 sm:gap-2 md:gap-3 p-1 md:p-6 bg-white border border-gray-200 shadow-xl dark:bg-gray-800 dark:border-gray-700">
              <div v-html="data.selectedPost.content.content"
                class="mx-auto text-gray-900 dark:text-gray-50 text-sm tracking-tight" :class="
                  data.selectedPost.content.data.image === ''
                    ? 'h-full'
                    : 'max-h-[25%] overflow-auto'
                "></div>
              <img v-if="data.selectedPost.content.data.image !== ''" :src="data.selectedPost.content.data.image"
                class="h-auto rounded-lg mx-auto" :class="
                  data.selectedPost.content.content.content === ''
                    ? 'max-h-[calc(50%-2rem)]'
                    : 'max-h-[20rem]'
                " alt="post image" />
            </div>

            <div class="w-full sm:w-3/4 md:w-1/2 mx-auto">
              <LikeCommentsButtons class="h-10" :likes="data.selectedPost.likesCount"
                :comments="data.selectedPost.commentsCount" :hasUserLike="data.selectedPost.hasUserLike"
                :isSavedByUser="data.selectedPost.isSavedByUser" @likeToggled="toggleLike()"
                @saveToggled="toggleSave()" />
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="flex flex-col mt-10 gap-y-8 w-full">
      <!-- <h1
        class="mx-auto mb-4 text-4xl font-extrabold tracking-tight leading-none md:text-5xl lg:text-6xl text-violet-600"
      >
        Post salvati
      </h1> -->
      <ul v-if="pending || data.status !== 200"
        class="grid sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-2 overflow-y-auto max-h-full">
        <li class="flex items-center justify-center w-full h-64 bg-gray-300 rounded-xl dark:bg-gray-700 px-2">
          <svg class="w-12 h-12 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor"
            viewBox="0 0 640 512">
            <path
              d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </li>
        <li class="flex items-center justify-center w-full h-64 bg-gray-300 rounded-xl dark:bg-gray-700 px-2">
          <svg class="w-12 h-12 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor"
            viewBox="0 0 640 512">
            <path
              d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </li>
        <li class="flex items-center justify-center w-full h-64 bg-gray-300 rounded-xl dark:bg-gray-700 px-2">
          <svg class="w-12 h-12 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor"
            viewBox="0 0 640 512">
            <path
              d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </li>
        <li class="flex items-center justify-center w-full h-64 bg-gray-300 rounded-xl dark:bg-gray-700 px-2">
          <svg class="w-12 h-12 text-gray-200" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" fill="currentColor"
            viewBox="0 0 640 512">
            <path
              d="M480 80C480 35.82 515.8 0 560 0C604.2 0 640 35.82 640 80C640 124.2 604.2 160 560 160C515.8 160 480 124.2 480 80zM0 456.1C0 445.6 2.964 435.3 8.551 426.4L225.3 81.01C231.9 70.42 243.5 64 256 64C268.5 64 280.1 70.42 286.8 81.01L412.7 281.7L460.9 202.7C464.1 196.1 472.2 192 480 192C487.8 192 495 196.1 499.1 202.7L631.1 419.1C636.9 428.6 640 439.7 640 450.9C640 484.6 612.6 512 578.9 512H55.91C25.03 512 .0006 486.1 .0006 456.1L0 456.1z" />
          </svg>
        </li>
      </ul>
      <ul v-else class="grid sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-2 max-h-full">
        <li v-for="post in data.posts">
          <button type="button" @click="openHighlightModal(post)" class="border border-gray-100 rounded-xl p-2">
            <div v-if="post.content.data.image === ''" v-html="post.content.content"
              class="mx-auto text-gray-900 dark:text-gray-50" :class="post.content.data.image === '' ? 'h-full' : ''">
            </div>
            <img v-else :src="post.content.data.image" class="h-auto max-h-full rounded-lg mx-auto" />
          </button>
        </li>
      </ul>
    </div>
  </NuxtLayout>
</template>
<script setup>
import { reactive } from "vue";
import { useRouter } from "vue-router";
import useFormat from "~/composables/useFormat";
import useParse from "~/composables/useParse";

const router = useRouter();
const data = reactive({
  posts: [],
  status: 401,
  selectedPost: {
    postId: 0,
    username: "",
    elapsed: "Ora",
    imageUrl: "",
    likes: [],
    comments: [],
    hasUserLike: false,
    isSavedByUser: true,
    likesCount: "0",
    commentsCount: "0",
    content: {
      content: "",
      data: {
        image: "",
      },
    },
  },
});

useHead({
  title: `${sessionStorage.getItem("username")} • MuzNet`,
  meta: [
    {
      name: "description",
      content:
        "Entra nel nostro social network professionale: connessioni globali con aziende di successo. Benvenuto!",
    },
  ],
});

const { pending } = useFetch(
  "https://progettoeasynet.azurewebsites.net/Save/GetSavedPosts",
  {
    lazy: true,
    server: false,
    method: "GET",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: "",
    },
    onRequest({ options }) {
      options.headers["Authorization"] = `Bearer ${sessionStorage.getItem(
        "token"
      )}`;
    },
    onResponse({ response }) {
      const { parsePost } = useParse();
      const username = sessionStorage.getItem("username");

      data.status = response.status;
      for (const post of response._data) {
        post.hasUserLike = getPostHasUserLike(post, username);
        post.isSavedByUser = true;
        post.content = parsePost(post.content);
        data.posts.push(post);
      }
    },
  }
);

function getPostHasUserLike(post, username) {
  return username !== null && username !== "" && post.likes.includes(username);
}

function openHighlightModal(post) {
  const { formatDate, formatNumber } = useFormat();

  data.selectedPost = post;
  data.selectedPost.elapsed = formatDate(data.selectedPost.dataDiCreazione);
  data.selectedPost.commentsCount = formatNumber(
    data.selectedPost.comments.length
  );
  data.selectedPost.likesCount = formatNumber(data.selectedPost.likes.length);

  const options = {};
  const highlightElement = document.getElementById("highlight-modal");
  const highlightModal = new Modal(highlightElement, options);
  highlightModal.show();
}

async function toggleLike() {
  const { formatNumber } = useFormat();

  data.selectedPost.hasUserLike = !data.selectedPost.hasUserLike;
  if (data.selectedPost.hasUserLike) {
    data.selectedPost.likes.push({});
  } else {
    data.selectedPost.likes.pop();
  }
  data.selectedPost.likesCount = formatNumber(data.selectedPost.likes.length);

  await useFetch(
    `https://progettoeasynet.azurewebsites.net/Like/PostLike?postId=${data.selectedPost.postId}`,
    {
      lazy: true,
      server: false,
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "",
      },
      onRequest({ options }) {
        options.headers["Authorization"] = `Bearer ${sessionStorage.getItem(
          "token"
        )}`;
      },
      onResponse({ response }) {
        if (!response.ok) {
          data.selectedPost.hasUserLike = !data.selectedPost.hasUserLike;
          if (data.selectedPost.hasUserLike) {
            data.selectedPost.likes.push({});
          } else {
            data.selectedPost.likes.pop();
          }
          data.selectedPost.likesCount = formatNumber(
            data.selectedPost.likes.length
          );
        }
      },
    }
  );
}
async function toggleSave() {
  data.selectedPost.isSavedByUser = !data.selectedPost.isSavedByUser;

  await useFetch(
    `https://progettoeasynet.azurewebsites.net/Save/PostSave?postId=${data.selectedPost.postId}`,
    {
      lazy: true,
      server: false,
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "",
      },
      onRequest({ options }) {
        options.headers["Authorization"] = `Bearer ${sessionStorage.getItem(
          "token"
        )}`;
      },
      onResponse({ response }) {
        if (!response.ok) {
          data.selectedPost.isSavedByUser = !data.selectedPost.isSavedByUser;
        }
      },
    }
  );
}

onMounted(() => {
  const token = sessionStorage.getItem("token");
  if (token === null || token === "" || token === "null") {
    router.push("/");
  }
});
definePageMeta({
  layout: true,
});
</script>
