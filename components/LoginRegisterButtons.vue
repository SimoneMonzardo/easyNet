<template>
  <div class="flex flex-row gap-x-2">
    <button type="button" data-modal-target="authentication-modal" data-modal-toggle="authentication-modal"
      class="bg-neutral-400 hover:bg-neutral-500 focus:ring-4 font-semibold focus:ring-neutral-300 font-medium rounded-full text-sm px-5 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Log in</button>
    <button type="button" data-modal-target="register-modal" data-modal-toggle="register-modal"
      class="bg-neutral-400 hover:bg-neutral-500 focus:ring-4 font-semibold focus:ring-neutral-300 font-medium rounded-full text-sm px-5 py-2 mr-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Register</button>
  </div>
</template>
  
<script>
export default {
  name: 'LoginRegisterButtons'
}
</script>
