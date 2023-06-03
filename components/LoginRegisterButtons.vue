<template>
  <div class="hidden sm:flex flex-row gap-x-2">
    <button type="button" data-modal-target="authentication-modal" data-modal-show="authentication-modal"
      class="bg-neutral-400 hover:bg-neutral-500 focus:ring-4 font-semibold focus:ring-neutral-300 font-medium rounded-xl text-sm px-5 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Log
      in</button>
    <button type="button" data-modal-target="register-modal" data-modal-show="register-modal"
      class="bg-neutral-400 hover:bg-neutral-500 focus:ring-4 font-semibold focus:ring-neutral-300 font-medium rounded-xl text-sm px-5 py-2 mr-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Register</button>
  </div>
  <div class="block sm:hidden">
    <button id="authDropdown" data-dropdown-toggle="signInActions" type="button">
      <UserCircleIcon class="h-6 w-6 text-gray-500 dark:text-gray-400 mt-1.5" />
    </button>
    <!-- Dropdown menu -->
    <div id="signInActions" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-28 dark:bg-gray-700">
      <ul class="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="authDropdown">
        <li class="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
          <button type="button" data-modal-target="authentication-modal" data-modal-toggle="authentication-modal" class="block px-4 py-2">
            Log in
          </button>
        </li>
        <li class="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">
          <button type="button" data-modal-target="register-modal" data-modal-toggle="register-modal" class="block px-4 py-2">
            Register
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
