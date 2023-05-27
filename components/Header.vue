<template>
  <header class="bg-slate-100 max-w-8xl dark:bg-gray-900 h-16">
    <nav class="mx-4 py-3">
      <div class="flex flex-wrap justify-between items-center">
        <div class="flex justify-start items-center">
          <a href="./" class="flex mr-4">
            <img src="https://flowbite.s3.amazonaws.com/logo.svg" class="mr-3 h-8" alt="FlowBite Logo" />
            <span class="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">easyNet</span>
          </a>
        </div>
        <div class="flex items-center lg:order-2">
          <ThemeToggle />
          <button data-drawer-target="sidebar" data-drawer-toggle="sidebar" data-drawer-placement="right"
            aria-controls="sidebar" id="sidebar-toggle"
            class="p-1.5 mr-2 text-gray-600 rounded-lg cursor-pointer sm:hidden hover:text-gray-900 hover:bg-gray-100 focus:bg-gray-100 dark:focus:bg-gray-700 focus:ring-2 focus:ring-gray-100 dark:focus:ring-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">
            <Bars3CenterLeftIcon class="h-6 w-6 text-gray-500 dark:text-gray-400 rotate-180" />
            <span class="sr-only">Attiva sidebar</span>
          </button>
          <div v-if="loggedIn">
            <button type="button"
              class="flex text-sm bg-gray-800 rounded-full md:mr-0 focus:ring-4 focus:ring-gray-300 dark:focus:ring-gray-600"
              id="user-menu-button" aria-expanded="false" data-dropdown-toggle="dropdown">
              <span class="sr-only">Apri menù utente</span>
              <img class="w-8 h-8 rounded-full" :src="imageUrl" alt="Immagine Utente" />
            </button>
            <!-- Dropdown menu -->
            <div
              class="hidden z-50 my-4 w-56 text-base list-none bg-white rounded divide-y divide-gray-100 shadow dark:bg-gray-700 dark:divide-gray-600"
              id="dropdown">
              <div class="py-3 px-4">
                <span class="block text-sm font-semibold text-gray-900 dark:text-white">{{ userName }}</span>
                <span class="block text-sm font-light text-gray-500 truncate dark:text-gray-400">{{ email }}</span>
              </div>
              <ul class="py-1 font-light text-gray-500 dark:text-gray-400" aria-labelledby="dropdown">
                <li>
                  <a href="#"
                    class="block py-2 px-4 text-sm hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 dark:hover:text-white">Profilo</a>
                </li>
                <li>
                  <a href="#"
                    class="block py-2 px-4 text-sm hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 dark:hover:text-white">Impostazioni
                    Account</a>
                </li>
              </ul>
              <ul class="py-1 font-light text-gray-500 dark:text-gray-400" aria-labelledby="dropdown">
                <li>
                  <a href="#"
                    class="block py-2 px-4 text-sm hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Esci</a>
                </li>
              </ul>
            </div>
          </div>
          <div v-else="loggedIn">
            <LoginRegisterButtons />
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<script>
import { Bars3CenterLeftIcon } from "@heroicons/vue/24/outline";

export default {
  props: {
    loggedIn: Boolean,
    email: String,
    userName: String,
    profilePicture: String
  },
  computed: {
    imageUrl() {
      return this.profilePicture;
    }
  },
  components: {
    Bars3CenterLeftIcon
  }
}
</script>
