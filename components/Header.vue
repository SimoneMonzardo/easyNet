<template>
  <header class="bg-violet-600 max-w-screen dark:bg-violet-800 h-16 shadow-lg shadow-[rgba(0,0,0,0.3)] dark:shadow-violet-950 top-0 w-screen" style="position: absolute; z-index: 20;">
    <nav class="mx-4 py-3">
      <div class="flex flex-wrap justify-between items-center">
        <div class="flex justify-start items-center">
          <a href="/" class="flex mr-4">
            <img
              src="~/public/logo.png"
              class="logo mr-3 h-8"
              alt="MuzNet Logo"
            />
            <span
              class="self-center text-2xl font-semibold whitespace-nowrap text-white"
              >MuzNet</span
            >
          </a>
        </div>
        <div class="flex items-center lg:order-2">
          <ThemeToggle class="hidden sm:block"/>
          <button
            data-drawer-target="filters-drawer"
            data-drawer-toggle="filters-drawer"
            data-drawer-placement="top"
            aria-controls="filters-drawer"
            id="filters-toggle"
            class="p-1.5 mr-2 text-gray-600 rounded-full cursor-pointer"
          >
            <MagnifyingGlassIcon
              class="h-6 w-6 text-gray-100 dark:text-gray-800"
            />
            <span class="sr-only">Attiva sidebar</span>
          </button>
          <div :class="loggedIn ? 'block' : 'hidden'">
            <button
              type="button"
              class="flex text-sm rounded-full md:mr-0 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-600"
              id="user-menu-button"
              data-dropdown-toggle="userDropdown"
            >
              <span class="sr-only">Apri menù utente</span>
              <UserCircleIcon v-if="profilePicture === '' || profilePicture === 'null'" class="h-8 w-8 my-auto text-gray-100 dark:text-gray-800" />
              <img
                v-else
                class="w-8 h-8 my-auto rounded-full overflow-x-hidden"
                :src="profilePicture"
                alt="Immagine Utente"
              />
            </button>
            <!-- Dropdown menu -->
            <div
              class="hidden z-50 my-4 w-56 text-base list-none bg-white rounded divide-y divide-gray-100 shadow dark:bg-gray-700 dark:divide-gray-600"
              id="userDropdown"
            >
              <div class="py-3 px-4">
                <span
                  class="block text-sm font-semibold text-gray-900 dark:text-white"
                  >{{ userName }}</span
                >
                <span
                  class="block text-sm font-light text-gray-500 truncate dark:text-gray-400"
                  >{{ email }}</span
                >
              </div>
              <ul
                class="py-1 font-light text-gray-500 dark:text-gray-400"
                aria-labelledby="user-menu-button"
              >
                <li>
                  <a
                    href="/saved"
                    class="block py-2 px-4 text-sm hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 dark:hover:text-white"
                    >Post salvati</a
                  >
                </li>
                <li>
                  <a
                    href="/settings"
                    class="block py-2 px-4 text-sm hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-400 dark:hover:text-white"
                    >Profilo</a
                  >
                </li>
              </ul>
              <ul
                class="py-1 font-light text-gray-500 dark:text-gray-400 w-full"
                aria-labelledby="user-menu-button"
              >
                <li>
                  <button
                    class="block w-full text-start text-red-600 hover:font-semibold py-2 px-4 text-sm hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-red-500"
                    @click="logOut"
                  >
                    Esci
                  </button>
                </li>
              </ul>
            </div>
          </div>
          <div :class="loggedIn ? 'hidden' : 'block'">
            <LoginRegisterButtons />
          </div>
        </div>
      </div>
      <button data-modal-target="forget-modal" hidden> </button>
      <button data-modal-target="success-modal" hidden> </button>
    </nav>
  </header>
</template>

<script>
import { MagnifyingGlassIcon } from "@heroicons/vue/24/outline";
import { UserCircleIcon } from "@heroicons/vue/24/outline";
import useStorage from '~/composables/useStorage';

export default {
  props: {
    loggedIn: Boolean,
    email: String,
    userName: String,
    profilePicture: String,
  },
  computed: {
    imageUrl() {
      return this.profilePicture;
    },
  },
  components: {
    UserCircleIcon,
    MagnifyingGlassIcon
  },
  methods: {
    logOut() {
      const { clearSession, clearLocal } = useStorage();
      clearSession();
      clearLocal();

      this.$router.go("/");
    },
  },
  mounted: function () { 
    const filterDrawerToggle = document.getElementById('filters-toggle');

    if (this.$nuxt._route.path != '/') {
      filterDrawerToggle.classList.add('hidden');
      return;
    }

    filterDrawerToggle.classList.remove('hidden');
  }
};
</script>

<style>
.logo:active {
  animation: spin 6s;
}

@keyframes spin {
  50% { transform: rotate(360deg); }
}
</style>
