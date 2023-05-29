<template>
  <nav class="mx-auto flex flex-row justify-evenly w-screen my-6 font-semibold text-2xl dark:text-white">
   <a href="./profile" class="">Profilo</a>
   <a href="./account" class="">Account</a>
   <a href="./privacy" class="">Privacy</a>
  </nav>
</template>

<script>
export default {
  name: "SettingsSelector"
}
</script>

<!-- Non rimuovere, senza non funziona -->
<style scoped>
   .active {
      text-decoration: underline;
   }
</style>
