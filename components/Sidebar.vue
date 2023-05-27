<template>
  <div id="sidebar" tabindex="-1" aria-labelledby="sidebar"
    class="absolute top-16 right-0 z-40 h-[calc(100vh-4rem)] w-screen p-4 overflow-y-auto aria-hidden:hidden transition-transform translate-x-full bg-white w-80 dark:bg-gray-800 sm:w-48 lg:w-64">
    <ul class="space-y-2 font-medium">
      <li class="text-center mx-auto mb-4">
        <Toggle text="Seguiti" />
      </li>
      <li>
        <form action="#" method="GET" class="block">
          <label for="search" class="sr-only">Cerca</label>
          <div class="relative mt-1 lg:w-48 mx-auto">
            <div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
              <MagnifyingGlassIcon class="h-6 w-6 text-gray-500" />
            </div>
            <input type="text" name="email" id="search"
              class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
              placeholder="Cerca" />
          </div>
        </form>
      </li>
    </ul>
  </div>
</template>

<script>
import { MagnifyingGlassIcon } from "@heroicons/vue/24/outline";
import { Drawer } from "flowbite";

export default {
  components: {
    MagnifyingGlassIcon
  },
  mounted: function () {
    this?.$nextTick(function () {
      this.onResize();
    });
    window.addEventListener('resize', this.onResize);
  },
  methods: {
    onResize() {
      const element = document.getElementById('sidebar');
      const options = {
        placement: 'right'
      }

      const sidebar = new Drawer(element, options);
      const sidebarToggle = document.getElementById('sidebar-toggle');

      if (this.$nuxt._route.path != '/') {
        sidebarToggle.classList.add('hidden');
        sidebar.hide();
        return;
      }

      sidebarToggle.classList.remove('hidden');

      if (window.innerWidth >= 640 && sidebar.isHidden()) {
        sidebar.show();
      } else {
        sidebar.hide();
      }
    }
  }
}
</script>
