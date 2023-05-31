<template>
  <footer class="bg-white dark:bg-gray-900">
    <div class="container mx-auto max-w-screen-6xl p-4 pb-2 lg:pb-3">
      <hr class="mb-2 border-gray-200 sm:mx-auto dark:border-gray-700 lg:mb-3" />
      <div class="sm:flex sm:items-center sm:justify-between text-center">
        <span class="text-sm text-gray-500 sm:text-center dark:text-gray-400">
          © 2023
          <a href="./" class="hover:underline">easyNet™</a>
        </span>
        <div class="mt-4 sm:mt-1">
           <ul class="flex flex-col sm:flex-row sm:gap-6 text-gray-600 dark:text-gray-400 font-medium items-center">
              <li class="mb-4 sm:mb-0">
                <a href="#" class="hover:underline">Privacy</a>
              </li>
              <li>             
                <!-- Modal toggle -->
                <button data-modal-target="terms-modal" data-modal-toggle="terms-modal" class="block text-dark focus:ring-0 focus:outline-none font-medium rounded-lg text-md px-5 py-2.5 text-center" type="button">
                  Termini &amp; Condizioni
                </button>
                <TermsConditionModal />
              </li>
              <li>
                <a href="https://github.com/SimoneMonzardo/easyNet/issues/new?assignees=&labels=bug&projects=&template=bug_report.yml" class="hover:underline">Report Bug</a>
              </li>
            </ul>
        </div>
      </div>
    </div>
  </footer>
</template>
