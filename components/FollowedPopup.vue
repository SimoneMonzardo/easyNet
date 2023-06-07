<template>
    <div>
        <button data-modal-target="medium-modal" data-modal-toggle="medium-modal"
            class="block w-full md:w-auto text-white bg-violet-700 hover:bg-violet-800 focus:ring-4 focus:outline-none focus:ring-violet-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-violet-700 dark:hover:bg-violet-700 dark:focus:ring-violet-800"
            type="button">
            Default modal
        </button>
    </div>
    <div id="medium-modal" tabindex="-1"
        class="fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-[calc(100%-1rem)]">
        <div class="relative w-full max-w-lg max-h-full">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 ">
                <!-- Modal header -->
                <div class="flex items-center justify-between p-5 border-b rounded-t dark:border-gray-600">
                    <h3 class="text-xl font-medium text-gray-900 dark:text-white">
                        Followed users
                    </h3>
                    <div class="relative m-2">
                    </div>
                    <button type="button"
                        class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white"
                        data-modal-hide="medium-modal">
                        <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20"
                            xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd"
                                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                                clip-rule="evenodd"></path>
                        </svg>
                        <span class="sr-only">Close modal</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="p-6 space-y-6 h-96 overflow-y-auto mb-5">
                    <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400 ">
                        <tbody>
                            <tr v-for="user in users"
                                class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                <th scope="row"
                                    class="flex items-center px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white">
                                    <img class="w-10 h-10 rounded-full"
                                        src="data:image/jpeg;base64,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"
                                        alt="Jese image">
                                    <div class="pl-3">
                                        <div class="text-base font-semibold">{{ user }}</div>
                                        <div class="font-normal text-gray-500">ConadTM</div>
                                    </div>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
const users = ["sbroffo", "marmello", "ardente", "clown", "sas", "sos", "ses", "sis", "sys", "clown", "sas", "sos", "ses", "sis", "sys"]
</script>
<script>
import { UserCircleIcon } from "@heroicons/vue/24/outline";

export default {
    name: 'LoginRegisterButtons',
    components: {
        UserCircleIcon
    },
    methods: {
        getFollowers() {

        }
    }
}
</script>