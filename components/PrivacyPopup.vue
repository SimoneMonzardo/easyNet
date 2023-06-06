<template>
  <!-- Privacy modal -->
  <div id="privacy-modal" tabindex="-1" aria-hidden="true" class="fixed bg-gray-900 bg-opacity-50 top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
    <div class="relative w-full max-w-3xl max-h-full">
      <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 flex flex-row">
        <div class="bg-gray-200 dark:bg-gray-800 flex-col content-center rounded-l-lg w-64 p-5 text-gray-900 justify-evenly hidden sm:flex">
          <h6 class="mx-auto text-4xl font-semibold text-violet-600">Mouzone</h6>
          <img src="~/public/logo.png" class="mt-3 h-15 rounded-full" alt="Mouzone Logo" />
        </div>
        <!-- Content -->
        <div class="w-full bg-white rounded-lg shadow dark:bg-gray-700">
          <!-- Header -->
          <div class="flex items-start justify-between p-4 border-b rounded-t dark:border-gray-600">
            <h3 class="text-xl font-semibold text-gray-900 dark:text-white">Privacy</h3>
            <button type="button" class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="privacy-modal">
              <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path>
              </svg>
              <span class="sr-only">Chiudi</span>
            </button>
          </div>
          <!-- Body -->
          <div class="p-6 space-y-6 overflow-y-scroll h-96 text-left">
            <p class="mt-2 text-lg font-semibold">POLITICA SULLA PRIVACY</p>
            <p> Ultimo aggiornamento: <i>31/05/2023</i></p>
            <b>La tua privacy è importante per noi. </b>
            <p> 
              Questa politica sulla privacy descrive come raccogliamo, utilizziamo, divulgiamo e proteggiamo
              le informazioni
              personali che ci fornisci quando utilizzi il nostro social Mouzone. <br>
              Si prega di leggere attentamente questa politica sulla privacy prima di utilizzare il Mouzone o
              fornirci qualsiasi informazione personale. <br>
              Per qualunque altra informazione ti preghiamo di contattare l'azienda tramite questo link: <a href="https://www.enet.it/contatti" target="_blank"> www.enet.it/contatti.</a>
            </p>
            <ul>
              <li>
                <p class="mt-2 text-lg font-semibold"> Raccolta delle informazioni personali</p>
                <p class="mt-2">
                  Raccogliamo le informazioni personali che ci fornisci volontariamente
                  durante la registrazione o l'utilizzo
                  del Sito. 
                </p>
                <p> Queste informazioni possono includere, ma non sono limitate a:</p>
                <ul class="ml-3">
                  <li class="mt-2">
                    <b>1. Informazioni di registrazione </b>: nome, indirizzo email, data di nascita, genere e altre
                    informazioni richieste durante il processo di registrazione.
                  </li>
                  <li>
                    <b>2. Profilo utente</b>: informazioni aggiuntive che scegli di fornire sul tuo
                    profilo utente, come foto, interessi, biografia e altro.
                  </li>
                  <li>
                    <b>3. Contenuti generati dall'utente </b>: le informazioni che pubblichi, carichi o
                    condividi tramite il Sito, come messaggi, commenti, foto, video e altri contenuti.
                  </li>
                  <li>
                    <b>4. Attività sul Sito </b>: informazioni sulle tue interazioni con il Sito,
                    inclusi i post visualizzati, i gruppi seguiti, gli eventi partecipati e altre attività.
                  </li>
                </ul>
              </li>
              <li>
                <p class="mt-2 text-lg font-semibold"> Utilizzo delle informazioni personali</p>
                <p class="mt-2">Utilizziamo le informazioni personali raccolte per le seguenti finalità:</p>
                <ul class="ml-3">
                  <li class="mt-2">1. Fornire, personalizzare e migliorare i servizi del Sito.</li>
                  <li>
                    2. Comunicare con te riguardo al tuo account, alle attività sul Sito e agli
                    aggiornamenti dei servizi.
                  </li>
                  <li>
                    3. Consentire interazioni sociali tra gli utenti del Sito, come la condivisione di
                    contenuti e la partecipazione a gruppi o eventi.
                  </li>
                  <li>4. Rispondere alle tue richieste, domande o commenti.</li>
                  <li>5. Invio di comunicazioni promozionali, previa tua autorizzazione.</li>
                  <li>6. Proteggere la sicurezza, la legalità e l'integrità del Sito e dei suoi utenti.</li>
                  <li>7. Ottemperare a obblighi legali e regolamentari.</li>
                </ul>
              </li>
              <li>
                <p class="mt-2 text-lg font-semibold">Divulgazione delle informazioni personali</p>
                <p class="mt-2">
                  Non vendiamo, affittiamo o divulgiamo le tue informazioni personali a terze
                  parti senza il tuo consenso, tranne nei seguenti casi:
                </p>
                <ul class="ml-3">
                  <li class="mt-2">
                    <b>1. Partner e fornitori di servizi</b> : possiamo condividere le informazioni con partner o fornitori di
                    servizi checollaborano con noi per fornire i servizi del Sito. Questi soggetti sono
                    obbligati a mantenere la riservatezza delle informazioni e utilizzarle solo per gli scopi concordati.
                  </li>
                  <li>
                    <b>2. Autorità legali</b> : possiamo divulgare le informazioni personali se
                    richiesto dalla legge, da un
                    processo legale o da un'indagine governativa.
                  </li>
                  <li>
                    <b>3. Protezione dei diritti e della sicurezza</b> : possiamo divulgare le
                    informazioni personali per
                    proteggere i nostri diritti.
                  </li>
                </ul>
              </li>
            </ul>
          </div>
          <!-- Footer -->
          <div class="flex items-center p-6 space-x-2 border-t border-gray-200 rounded-b dark:border-gray-600">
            <button
              data-modal-hide="privacy-modal"
              type="button"
              class="text-white bg-violet-600 hover:bg-violet-700 focus:ring-4 focus:outline-none focus:ring-violet-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-violet-700 dark:hover:bg-violet-700 dark:focus:ring-violet-800">
              Accetta
            </button>
            <button
              data-modal-hide="privacy-modal"
              type="button"
              class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-violet-300 rounded-lg border border-gray-200 text-sm font-medium px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
              Rifiuta
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
