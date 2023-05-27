




<template>
<section class="bg-white dark:bg-gray-900">
   <div class="max-w-2xl px-4 py-8 mx-auto lg:py-16">
      <vue-tailwind-datepicker i18n="fr" v-model="dateValue" />
      <div class="flex pb-4">
         <h2>
            <a class ="mr-4 text-xl font-bold text-gray-900 dark:text-white">
            Profilo
            </a>
         </h2>
      </div>
      <div>
         <div class="grid gap-4 mb-4 sm:grid-cols-2 sm:gap-6 sm:mb-5">
            <div class="w-full">
               <label for="nome" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Nome</label>
               <input type="text" name="nome" id="nome" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Inserisci il tuo nome" required="">
            </div>
            <div class="w-full">
               <label for="cognome" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Cognome</label><input type="text" name="cognome" id="cognome" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Inserisci il tuo cognome" required="">
            </div>
            <div class="w-full">
               <label for="genere" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Genere</label>
               <select class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Inserisci il tuo genere" required="" name="genere" id="genere">
                  <option selected disabled>Inserisci genere</option>
                  <option>Maschio</option>
                  <option>Femmina</option>
               </select>
            </div>
            <div class="w-full">
               <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Data di Nascita</label>
               <input type="datetime-local" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Inserisci il tuo genere" required="" name="data" id="data">
            </div>
            <div class="sm:col-span-2">
               <label for="description" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Descrizione</label>
               <textarea id="description" rows="8" class="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-primary-500 focus:border-primary-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Scrivi una descrizione del profilo..."></textarea>
            </div>
         </div>
         <div class="w-full flex items-center">
            <button class="bg-red-500 hover:bg-red-600 text-gray-1000 font-bold py-2 px-4 mx-10 ml-0 rounded">
            Cancella profilo
            </button>
            <div class="inline-flex">
               <button class="bg-gray-300 hover:bg-gray-400 text-gray-1000 font-bold py-2 px-4 ml-12 rounded-l">
               Annulla
               </button>
               <button class="bg-green-400 hover:bg-green-500 text-gray-1000 font-bold py-2 px-4 rounded-r">
               Salva
               </button>
            </div>
         </div>
      </div>
   </div>
</section>

</template>



<script>
export default {
  name: "SettingsSelector"
};
</script>



<style scoped>
/* non rimuovere, senza esplode */
</style>





