<template onMounted="loaded()">
  <Header
    :logged="logged"
    :email="email" 
    :userName="username"
    :profilePicture="profilePicture" />
  <div class="min-h-[calc(100vh-4rem)] flex flex-row">
    <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
    <Sidebar />
  </div>
  <Footer />
</template>

<script setup>
import { onMounted } from "vue";
import { initFlowbite } from "flowbite";

onMounted(() => {
  initFlowbite();
});
</script>

<script>
  export default {
    data: () => ({
      logged: false,
      username: '',
      email: '',
      profilePicture: ''
    }),
    methods: {
      loaded() {
        this.logged = localStorage.getItem('logged');
        if (this.logged === null) {
          this.logged = false;
        }

        this.username = localStorage.getItem('username');
        if (this.username === null) {
          this.username = '';
        }

        this.email = localStorage.getItem('email');
        if (this.email === null) {
          this.email = '';
        }

        this.profilePicture = localStorage.getItem('profilePicture');
        if (this.profilePicture === null) {
          this.profilePicture = '';
        }
      }
    }
  }
</script>
