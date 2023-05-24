<template>
  <div class="">
    <div class="">
      <Header />
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
