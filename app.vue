<template>
  <div class="">
    <div class="">
      <Header email="temp@mail.com" userName="Temp User" profilePicture="https://st2.depositphotos.com/1007995/8863/i/600/depositphotos_88637478-stock-photo-side-profile-picture-of-a.jpg" />
    </div>
    <div>
      <NuxtLayout>
        <NuxtPage />
      </NuxtLayout>
    </div>
    <div class="">
      <Footer />
    </div>
  </div>
</template>
<script setup>
import { onMounted } from "vue";
import { initFlowbite } from "flowbite";

onMounted(() => {
  initFlowbite();
});
</script>
