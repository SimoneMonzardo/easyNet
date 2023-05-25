<template>
  <footer class="bg-white dark:bg-gray-900">
    <div class="container mx-auto max-w-screen-6xl p-4 py-6 lg:py-8">
      <div class="md:flex md:justify-between">
        <div class="mb-6 md:mb-0">
          <a href="./" class="flex items-center">
            <!-- TODO: Use logo -->
            <img
              src="https://flowbite.com/docs/images/logo.svg"
              class="h-8 mr-3"
              alt="easyNet Logo"
            />
            <span
              class="self-center text-2xl font-semibold whitespace-nowrap dark:text-white"
              >easyNet</span
            >
          </a>
        </div>
        <div class="grid grid-cols-2 gap-8 sm:gap-6 sm:grid-cols-3">
          <div>
            <h2
              class="mb-6 text-sm font-semibold text-gray-900 uppercase dark:text-white"
            >
              Risorse
            </h2>
            <ul class="text-gray-600 dark:text-gray-400 font-medium">
              <li class="mb-4">
                <a href="https://flowbite.com/" class="hover:underline"
                  >Flowbite</a
                >
              </li>
              <li>
                <a href="https://tailwindcss.com/" class="hover:underline"
                  >Tailwind CSS</a
                >
              </li>
            </ul>
          </div>
          <div>
            <h2
              class="mb-6 text-sm font-semibold text-gray-900 uppercase dark:text-white"
            >
              Seguici
            </h2>
            <ul class="text-gray-600 dark:text-gray-400 font-medium">
              <li class="mb-4">
                <a
                  href="https://github.com/SimoneMonzardo/easyNet"
                  class="hover:underline"
                  >Github</a
                >
              </li>
            </ul>
          </div>
          <div>
            <h2
              class="mb-6 text-sm font-semibold text-gray-900 uppercase dark:text-white"
            >
              Informazioni Legali
            </h2>
            <ul class="text-gray-600 dark:text-gray-400 font-medium">
              <li class="mb-4">
                <a href="#" class="hover:underline">Privacy Policy</a>
              </li>
              <li>
                <a href="#" class="hover:underline">Termini &amp; Condizioni</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <hr
        class="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8"
      />
      <div class="sm:flex sm:items-center sm:justify-between">
        <span class="text-sm text-gray-500 sm:text-center dark:text-gray-400"
          >© 2023
          <a href="./" class="hover:underline">easyNet™</a>
        </span>
      </div>
    </div>
  </footer>
</template>
