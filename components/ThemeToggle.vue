<script setup>
import { SunIcon, MoonIcon } from "@heroicons/vue/24/solid";
import { useDark, useToggle } from "@vueuse/core";

const isDark = useDark();
const toggleDark = useToggle(isDark);
</script>

<template>
  <div>
    <button type="button" @click="toggleDark()">
      <SunIcon v-if="isDark" class="h-6 w-6 text-gray-500" />
      <MoonIcon v-else class="h-6 w-6 text-black" />
    </button>
  </div>
</template>
