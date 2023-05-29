<template>
  <head>
    <title>Impostazioni #Profilo • easyNet</title>
  </head>

  <NuxtPage />

</template>

<script>
  export default {
    created() {
      this.$router.push('/settings/profile');
    }
  }
</script>
