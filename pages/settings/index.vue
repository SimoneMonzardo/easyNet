<template>
  <head>
    <title>Impostazioni #Profilo • easyNet</title>
  </head>
  <section Modals>
    <RegisterPopup />
    <LoginPopup />
  </section>

  <NuxtPage />

</template>

<script>
  export default {
    created() {
      this.$router.push('/settings/profile');
    }
  }
</script>
