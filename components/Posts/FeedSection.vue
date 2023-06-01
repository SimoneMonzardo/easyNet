<template>
  <div class="grid grid-cols-1 md:grid-cols-3 xl:grid-cols-4 gap-1 sm:gap-4 max-h-[calc(100vh-16rem)]">
    <PostHeader :username="post.username" :elapsedTime="elapsed" class="col-span-1 md:col-span-2 xl:col-span-3 order-1" />

    <div class="col-span-1 md:col-span-2 xl:col-span-3 order-2">
      <img class="rounded-xl w-full"
        src="https://wallpaper-mania.com/wp-content/uploads/2018/09/High_resolution_wallpaper_background_ID_77701353282.jpg" />
    </div>

    <div class="order-3 h-full mx-auto w-full 2xl:w-4/5 flex flex-col gap-2">
      <LikeCommentsButtons :likes="likes" :comments="comments" />

      <div class="shadow-inner shadow-gray-500 rounded-xl bg-gray-400 flex flex-col justify-between h-full">
        <div>

        </div>
        <form class="w-full">
          <label for="chat" class="sr-only">Commenta...</label>
          <div class="flex items-center px-2 bg-gray-500 bg-opacity-20 rounded-xl">
            <textarea id="chat" rows="1"
              class="block py-1 px-1.5 w-full text-sm text-gray-900 bg-white rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              placeholder="Commenta..." style="resize: none"></textarea>
            <button @click="postComment()" type="submit"
              class="inline-flex justify-center p-2 text-blue-600 rounded-full cursor-pointer hover:bg-blue-100 dark:text-blue-500 dark:hover:bg-gray-600">
              <svg aria-hidden="true" class="w-6 h-6 rotate-90" fill="white" viewBox="0 0 20 20"
                xmlns="http://www.w3.org/2000/svg">
                <path
                  d="M10.894 2.553a1 1 0 00-1.788 0l-7 14a1 1 0 001.169 1.409l5-1.429A1 1 0 009 15.571V11a1 1 0 112 0v4.571a1 1 0 00.725.962l5 1.428a1 1 0 001.17-1.408l-7-14z">
                </path>
              </svg>
              <span class="sr-only">Invia</span>
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
const suffixes = ['', ' K', 'M'];

export default {
  props: {
    post: {}
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
    }
  },
  methods: {
    async postComment() {

    }
  }
}
</script>
