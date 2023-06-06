<template>
      <h3 class=" transition-colors mb-3 py-20 text-4xl text-gray-900 dark:text-white">Email confermata</h3>
</template>

<script setup>
import { reactive } from 'vue';
import { useRouter } from "vue-router";
import useFormat from '~/composables/useFormat';
import * as matter from 'gray-matter';

</script>
<script>
export default {
  head: {
    title:"Mail"
  }
}
</script>