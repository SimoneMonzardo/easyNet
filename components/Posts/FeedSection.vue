<template>
  <div class="flex flex-col md:grid md:grid-cols-3 xl:grid-cols-4 gap-1 sm:gap-2 md:gap-3 max-h-[calc(100vh-16rem)]">
    <PostHeader :username="post.username" :elapsedTime="elapsed" class="col-span-1 md:col-span-2 xl:col-span-3 order-1" />

    <!-- TODO: Remove bg color. It's there as a placholder -->
    <div class="col-span-1 md:col-span-2 xl:col-span-3 order-2 h-[calc(30rem)] max-h-[calc(35vh)] md:max-h-full flex justify-center bg-red-900" v-html="content">
    </div>

    <div class="order-3 h-full mx-auto w-full 2xl:w-4/5 flex flex-col gap-1 sm:gap-2 md:gap-3 h-[calc(50rem)]">
      <LikeCommentsButtons
        :likes="likes"
        :comments="comments"
        :hasUserLike="post.hasUserLike"
        @likeToggled="toggleLike" />

      <div class="shadow-inner shadow-gray-500 rounded-xl bg-gray-400 flex flex-col justify-between h-[calc(20vh)] md:h-full">
        <div>

        </div>
        <div class="w-full">
          <label for="chat" class="sr-only">Commenta...</label>
          <div class="flex items-center px-2 bg-gray-500 bg-opacity-20 rounded-xl">
            <textarea
              v-model="userComment"
              id="chat" 
              rows="1"
              class="block py-1 px-1.5 w-full text-sm text-gray-900 bg-white rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              placeholder="Commenta..." 
              style="resize: none"></textarea>
            <button 
              @click="postComment()" 
              type="submit"
              class="inline-flex justify-center p-2 text-blue-600 rounded-full cursor-pointer hover:bg-blue-100 dark:text-blue-500 dark:hover:bg-gray-600">
              <PaperAirplaneIcon class="w-6 h-6 text-gray-100" />
              <span class="sr-only">Invia</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { PaperAirplaneIcon } from "@heroicons/vue/24/outline";
import { Markdown } from 'vue3-markdown-it';

const suffixes = ['', ' K', 'M'];

export default {
  props: {
    post: {}
  },
  data: () => ({
    userComment: ''
  }),
  components: {
    PaperAirplaneIcon,
    Markdown
  },
  computed: {
    elapsed() {
      if (this.post.dataDiCreazione === null) {
        return "Ora";
      }
    },
    likes() {
      var suffixIndex = 0;
      var likesCount = this.post.likes.length;
      while (likesCount > 999) {
        likesCount = Math - floor(likesCount / 1000);
        suffixIndex++;
      }

      return likesCount + suffixes[suffixIndex];
    },
    comments() {
      var suffixIndex = 0;
      var commentsCount = this.post.comments.length;
      while (commentsCount > 999) {
        commentsCount = Math - floor(commentsCount / 1000);
        suffixIndex++;
      }

      return commentsCount + suffixes[suffixIndex];
    },
    content() {
      // return '<img class="rounded-xl max-h-full" src="https://wallpaper-mania.com/wp-content/uploads/2018/09/High_resolution_wallpaper_background_ID_77701353282.jpg" />';
      return this.post.content;
    }
  },
  methods: {
    async postComment() {
      const comment = {
        postId: this.post.postId,
        commentId: 0,
        content: this.userComment
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
          if (response.status === 200) {
            //this.post.comments.append()
            // TODO: Refresh Comment List
          } else if (response.status === 401) {
            // TODO: Show login modal, create a common method to reuse
          }
        }
      });
    },
    async toggleLike() {
      await useFetch('https://progettoeasynet.azurewebsites.net/Like/PostLike', {
        lazy: true,
        server: false,
        method: 'POST',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        body: JSON.stringify({ postId: this.post.postId }),
        onRequest({ options }) {
          options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
        },
        onResponse({ response }) {
          if (response.status === 200) {
            this.post.hasUserLike = !this.post.hasUserLike;
          } else if (response.status === 401) {
            // TODO: Show login modal, create a common method to reuse
          }
        }
      });
    }
  }
}
</script>
