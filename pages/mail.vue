<template>
  <h3
    class="place-items-center transition-colors mb-3 py-20 text-4xl text-gray-900 dark:text-white"
    id="Risp"
  >
      Processando...
  </h3>
</template>

<script setup>
import { reactive } from "vue";
import { useRouter } from "vue-router";
import useFormat from "~/composables/useFormat";
import * as matter from "gray-matter";
</script>
<script>
export default {
  head: {
    title: "Mail",
  },
  async beforeMount() {
    console.log("montato");
    const id = this.$route.query.userId;
    console.log(id);
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
          onRequest({ request, options }) {
            // Set the request header
            console.log("richiesta");
          },
          onRequestError({ request, options, error }) {
            // Handle the request
          },
          onResponse({ request, response, options }) {
            console.log(response._data);
            if(response._data == "Email confirmed succesfully"){
                document.getElementById("Risp").innerHTML = "Email confermata";
            }
            else{
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
