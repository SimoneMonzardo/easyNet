<script setup>
import { useDark, useToggle } from "@vueuse/core";
import { SunIcon, MoonIcon } from "@heroicons/vue/20/solid";

const isDark = useDark();
const toggleDark = useToggle(isDark);
</script>

<template>
  <div>
    <button type="button" @click="toggleDark()">
      <SunIcon v-if="isDark" class="h-6 w-6 text-gray-400" />
      <MoonIcon v-else-if="!isDark" class="h-6 w-6 text-gray-500" />
    </button>
  </div>
</template>
