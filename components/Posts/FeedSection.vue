<template>
  <div class="flex flex-col md:grid md:grid-cols-3 xl:grid-cols-4 gap-1 sm:gap-2 md:gap-3 max-h-[calc(100vh-16rem)]">
    <PostHeader :username="post.username" :elapsedTime="elapsed" class="col-span-1 md:col-span-2 xl:col-span-3 order-1" />

    <!-- TODO: Remove bg color. It's there as a placholder -->
    <div class="col-span-1 md:col-span-2 xl:col-span-3 order-2 h-[calc(30rem)] max-h-[calc(35vh)] md:max-h-full flex justify-center bg-red-900 rounded-xl" v-html="content">
    </div>

    <div class="order-3 h-full mx-auto w-full 2xl:w-4/5 flex flex-col gap-1 sm:gap-2 md:gap-3 h-[calc(50rem)]">
      <LikeCommentsButtons
        :likes="likes"
        :comments="comments"
        :hasUserLike="post.hasUserLike"
        @likeToggled="toggleLike" />

      <div class="shadow-inner shadow-gray-500 rounded-xl bg-gray-400 flex flex-col justify-between h-[calc(20vh)] md:h-full">
        <ul class="tracking-tight text-white overflow-y-scroll max-h-[calc(22rem)] my-4">
          <li v-for="comment in post.comments" class="w-full px-4 py-2">
            <h6 class="text-md font-semibold">{{ comment.username }}</h6>
            <p class="text-xs leading-tight">{{ comment.content }}</p>
          </li>
        </ul>
        <div class="w-full">
          <label for="addAComment" class="sr-only">Commenta...</label>
          <div class="flex items-center px-2 bg-gray-500 bg-opacity-20 rounded-xl">
            <textarea
              v-model="userComment"
              id="addAComment" 
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

      const postDate = new Date(this.post.dataDiCreazione);
      const currentDate = new Date();

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
          document.getElementById('addAComment').value = '';
          // this.$data.userComment = '';

          // if (response.status === 200) {
          //   this.$props.post.comments.push({
          //     username: localStorage.getItem('username'),
          //     content: comment.content
          //   });
          // } else if (response.status === 401) {
          //   // TODO: Show login modal, create a common method to reuse
          // }
        }
      });
    },
    async toggleLike() {
      const { error } = await useFetch(`https://progettoeasynet.azurewebsites.net/Like/PostLike?postId=${this.$props.post.postId}`, {
        lazy: true,
        server: false,
        method: 'POST',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': ''
        },
        onRequest({ options }) {
          options.headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
        },
        onResponse({ response }) {
          console.log(response);
          console.log()
          //console.log($data);
          // try {
          // if (response.ok) {
          //   console.log(Object.keys(this));
          //   this.$props.post.hasUserLike = !this.$props.post.hasUserLike;
          //   if (this.post.hasUserLike) {
          //     this.$props.post.likes.push({});
          //   } else {
          //     this.$props.post.likes.pop();
          //   }
          // } else if (response.status === 401) {
          //   console.log('Errore');
          //   // TODO: Show login modal, create a common method to reuse
          // }
          console.log('fine');
        // } catch (error) {
        //     console.log(error);
        //   }
        }
      });
      console.log(error);
    }
  }
}
</script>
