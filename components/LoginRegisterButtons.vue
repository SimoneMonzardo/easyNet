<template>
  <div class="hidden sm:flex flex-row gap-x-2">
    <button type="button" data-modal-target="authentication-modal" data-modal-show="authentication-modal"
      class="text-gray-900 bg-gray-100 focus:ring-4 font-semibold focus:ring-neutral-300 rounded-xl text-sm px-5 py-2 focus:outline-none hover:bg-gray-200 dark:bg-gray-600 dark:shadow-gray-800 dark:border-gray-900 dark:text-gray-50 dark:hover:bg-gray-700">Log
      in</button>
    <button type="button" data-modal-target="register-modal" data-modal-show="register-modal"
      class="text-gray-900 bg-gray-100 focus:ring-4 font-semibold focus:ring-neutral-300 rounded-xl text-sm px-5 py-2 mr-2 focus:outline-none hover:bg-gray-200 dark:bg-gray-600 dark:shadow-gray-800 dark:border-gray-900 dark:text-gray-50 dark:hover:bg-gray-700">Register</button>
  </div>
  <div class="block sm:hidden">
    <button id="authDropdown" data-dropdown-toggle="signInActions" type="button">
      <UserCircleIcon class="h-6 w-6 text-gray-100 dark:text-gray-800 mt-1.5" />
    </button>
    <!-- Dropdown menu -->
    <div id="signInActions" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-xl shadow w-28 dark:bg-gray-700">
      <ul class="py-2 text-sm text-gray-900 dark:text-gray-50" aria-labelledby="authDropdown">
        <li class="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
          <button type="button" data-modal-target="authentication-modal" data-modal-toggle="authentication-modal" class="block px-4 py-2">
            Log in
          </button>
        </li>
        <li class="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
          <button type="button" data-modal-target="register-modal" data-modal-toggle="register-modal" class="block px-4 py-2">
            Registrati
          </button>
        </li>
      </ul>
    </div>
  </div>
</template>
  
<script>
import { UserCircleIcon } from "@heroicons/vue/24/outline";

export default {
  name: 'LoginRegisterButtons',
  components: {
    UserCircleIcon
  }
}
</script>
