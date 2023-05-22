<template>
<section class="bg-white dark:bg-gray-900">
  <div class="max-w-2xl px-4 py-8 mx-auto lg:py-16">
    

  <div class="flex pb-4">
    <h2>
      <a
        :class="['mr-4', 'text-xl', 'font-bold', 'text-gray-900', 'dark:text-white', {'underline': activeButton === 'profilo'}]"
        @click="selectButton('profilo')"
        href="#"
      >
        Profilo
      </a>
    </h2>
    <h2>
      <a
        :class="['mr-4', 'text-xl', 'font-bold', 'text-gray-900', 'dark:text-white', {'underline': activeButton === 'privacy'}]"
        @click="selectButton('privacy')"
        href="#"
      >
        Privacy
      </a>
    </h2>
    <h2>
      <a
        :class="['text-xl', 'font-bold', 'text-gray-900', 'dark:text-white', {'underline': activeButton === 'notifiche'}]"
        @click="selectButton('notifiche')"
        href="#"
      >
        Notifiche
      </a>
    </h2>
  </div>

  <div>
      <form v-if="activeButton === 'profilo'" action="#">
          <div class="grid gap-4 mb-4 sm:grid-cols-2 sm:gap-6 sm:mb-5">
              <div class="sm:col-span-2">
                  <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Nome Profilo</label>
                  <input type="text" name="name" id="name" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Insert il nome del profilo" required="">
              </div>
              <div class="w-full">
                  <label for="brand" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Numero di Telefono</label>
                  <input type="text" name="brand" id="brand" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Inserisci il tuo numero di telefono" required="">
              </div>
              <div class="w-full">
                  <label for="price" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email</label>
                  <input type="number" name="price" id="price" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" value="" placeholder="Inserisci la tua email" required="">
              </div>
              <div class="sm:col-span-2">
                  <label for="description" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Descrizione</label>
                  <textarea id="description" rows="8" class="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-primary-500 focus:border-primary-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Scrivi una descrizione del profilo..."></textarea>
              </div>
          </div>
      </form>
      
      <form v-if="activeButton === 'privacy'" action="#">

      </form>
      
      <form v-if="activeButton === 'notifiche'" action="#">
        <label for="name" class="block mb-4 text-sm font-medium text-gray-900 dark:text-white">Mostra Notifiche per</label>
        <div class="grid gap-4 mb-4 sm:grid-cols-1 sm:gap-6 sm:mb-5">
          <div class="w-full flex items-center">

            <label class="relative padding-right: 10px inline-flex items-center cursor-pointer">
              <input type="checkbox" value="" class="sr-only peer">
              <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
              <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">Messaggi</span>
            </label>
            <p class="padding-left: 100px;">invieremo una notifica quando un utente vi invierà un messaggio</p>
          </div>
          <div class="w-full flex items-center">

            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" value="" class="sr-only peer">
              <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
              <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">Menzioni</span>
            </label>

          </div>
          <div class="w-full flex items-center">

            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" value="" class="sr-only peer">
              <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
              <span class="ml-3 text-sm font-medium text-gray-900 dark:text-gray-300">Commenti</span>
            </label>

          </div>
        </div>
      </form>
    </div>


</div>
</section>
</template>

<script>
export default {
  name: 'SettingSelector',
  data() {
    return {
      activeButton: '',
      activeButtonValue: ''
    };
  },
  methods: {
    selectButton(button) {
      this.activeButton = button;
      this.activeButtonValue = button;
    }
  }
};
</script>

<style scoped>
.underline {
  text-decoration: underline;
}
</style>





