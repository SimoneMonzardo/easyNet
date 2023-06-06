<script setup>
import { useDark, useToggle } from "@vueuse/core";
import { SunIcon, MoonIcon } from "@heroicons/vue/20/solid";

const isDark = useDark();
const toggleDark = useToggle(isDark);
</script>

<template>
  <div>
    <button type="button" @click="toggleDark()" class="mt-1.5 mr-2" name="theme-toggle">
      <SunIcon v-if="isDark" class="h-6 w-6 text-gray-800" />
      <MoonIcon v-else-if="!isDark" class="h-6 w-6 text-gray-100" />
    </button>
  </div>
</template>
