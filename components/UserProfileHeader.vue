<template>
    <div class="max-w p-8 sm:flex sm:space-x-6 dark:bg-gray-900 dark:text-gray-100 place-content-center"  >
        <div class="flex flex-col space-y-4" style="margin-right:40%; ">
            <div style="display:flex">
                <img class="w-40 h-40 mb-3 rounded-full shadow-lg" :src="image" alt="user image"/>
                <div>
                <h2 class="text-3xl font-semibold" style="margin-left:50px; " >{{ user_name }}</h2>
                    <div style="display:flex; margin-top:4%">
                        <h2 class="text-xl font-semibold" style="margin-left:50px; margin-top:1%">Followers: {{ followers_count }} </h2>                     
                        <h2 class="text-xl font-semibold" style="margin-left:50px; margin-top:1%">Following: {{ following_count }} </h2>
                    </div>
                </div>        
            </div>
            <span class="text-sm dark:text-gray-400">{{ description }}</span>
            <div class="flex mt-4 space-x-3 md:mt-6">
                    <div v-if="haveBanner" style="float: left">
                        <button type="button" style="width:120px" class="inline-flex items-center px-4 py-2 text-sm font-medium text-center text-white bg-blue-700 rounded-xl hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                            <a href="#" style="margin:auto">Follow</a>
                        </button>
                    </div>
                    
                    <div v-if="haveBanner" style="float: left; margin-left:20%">
                        <button style="width:120px" class="inline-flex items-center px-4 py-2 text-sm font-medium text-center text-gray-900 bg-white border border-gray-300 rounded-xl hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-700 dark:focus:ring-gray-700">
                            <a href="#" style="margin:auto">Messaggio</a>
                        </button>
                    </div>
                    <div v-if="haveBanner"  style="float: left; margin-left:20%">
                        <button style="width:120px" class="inline-flex items-center px-4 py-2 text-sm font-medium text-center text-gray-900 bg-white border border-gray-300 rounded-xl hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-700 dark:focus:ring-gray-700">
                            <a href="#" style="margin:auto">Email</a>
                        </button>
                    </div>
                    <div v-else="haveBanner"  style="float: left;">
                        <button style="width:120px" class="inline-flex items-center px-4 py-2 text-sm font-medium text-center text-gray-900 bg-white border border-gray-300 rounded-xl hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-200 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-700 dark:focus:ring-gray-700">
                            <a href="#" style="margin:auto">sas</a>
                        </button>
                    </div>                  
            </div>
        </div>       
    </div>
</template>

<script>
export default {
  props: 
  {
    user_name :String,
    profile_picture: String,
    followers_count:String,
    following_count:String,
    description:String, 
    haveBanner: Boolean
  }, 
  computed:{
    image(){
        return this.profile_picture;
    }
  }
}
</script>