<template>
  <LoginPopup />
  <RegisterPopup />
  <ForgetPopup />
  <SuccessPopup />
  <h3 class="place-items-center transition-colors mb-3 py-20 text-4xl text-gray-900 dark:text-white" id="Risp">
    Processando...
  </h3>
</template>

<script>
export default {
  head: {
    title: "Mail",
  },
  async beforeMount() {
    const id = this.$route.query.userId;
    await this.api(id);
  },
  methods: {
    async api(id) {
      await useFetch(
        "https://progettoeasynet.azurewebsites.net/Auth/confirmEmail?userId=" + id,
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
          },
          method: "GET",
          onResponse({ response }) {
            if (response._data == "Email confirmed succesfully") {
              document.getElementById("Risp").innerHTML = "Email confermata";
            } else {
              document.getElementById("Risp").innerHTML = "Errore nella conferma del''email";
            }
            this.$router.go();
          },
        }
      );
    },
  },
};
</script>
