<template>
  <!-- Terms & Conditions modal -->
  <div id="terms-modal" tabindex="-1" aria-hidden="true"
    class="fixed bg-gray-900 bg-opacity-50 top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full">
    <div class="relative w-full max-w-3xl max-h-full">
      <!-- Content -->
      <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 flex flex-row">
        <div
          class="bg-gray-200 dark:bg-gray-800 flex-col content-center rounded-l-lg w-64 p-5 text-gray-900 justify-evenly hidden sm:flex">
          <h6 class="mx-auto text-4xl font-semibold text-violet-600">MuzNet</h6>
          <img v-if="isDark" src="~/public/muznet-white.png" class="mt-3 h-15 rounded-full" alt="MuzNet Logo" />
          <img v-else-if="!isDark" src="~/public/muznet-black.png" class="mt-3 h-15 rounded-full" alt="MuzNet Logo" />
        </div>
        <div class="w-full">
          <!-- Header -->
          <div class="flex items-start justify-between p-4 border-b rounded-t dark:border-gray-600">
            <h3 class="text-xl font-semibold text-gray-900 dark:text-white">Termini di servizio</h3>
            <button type="button"
              class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white"
              data-modal-hide="terms-modal">
              <svg aria-hidden="true" class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20"
                xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd"
                  d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                  clip-rule="evenodd"></path>
              </svg>
              <span class="sr-only">Chiudi</span>
            </button>
          </div>
          <!-- Body -->
          <div class="p-6 space-y-6 overflow-y-scroll h-96">
            <p class="text-base text-left leading-relaxed text-gray-500 dark:text-gray-400">
              Termini e Condizioni di Utilizzo di MuzNet
              Benvenuto in MuzNet, da qui in avanti "servizio", "social". Prima di utilizzare il nostro servizio,
              ti preghiamo di leggere attentamente i seguenti Termini e Condizioni che disciplinano l'uso del
              social. L'utilizzo di MuzNet implica l'accettazione incondizionata di tali Termini da parte
              dell'utente. Nel caso in cui l'Utente non accetti uno o più dei presenti Termini, si prega di
              astenersi dall'utilizzo del social.
            <ul>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">1. Accettazione dei Termini</h4>
                <p>
                  L'accesso e l'utilizzo di MuzNet sono consentiti solo agli utenti che accettano di essere
                  vincolati dai presenti Termini. L'Utente conferma di essere maggiorenne e di avere la
                  capacità legale di accettare tali Termini. L'utilizzo del social da parte dell'Utente implica
                  l'accettazione totale e senza riserve di tutti i Termini qui descritti.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">2. Registrazione e Account</h4>
                <p>
                  Per utilizzare alcune funzionalità di MuzNet, l'Utente potrebbe dover creare un account.
                  L'Utente è tenuto a fornire informazioni accurate, complete e aggiornate durante la procedura di
                  registrazione. L'Utente è responsabile della riservatezza dei propri dati di accesso e si
                  impegna a non condividere tali informazioni con terzi. L'Utente è l'unico responsabile delle
                  attività che si svolgono sul proprio account.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">3. Utilizzo di MuzNet</h4>
                <p>
                  L'Utente si impegna a utilizzare il social in conformità alle leggi e ai regolamenti
                  applicabili, nonché alle presenti disposizioni. L'Utente accetta di non compiere alcuna
                  azione che possa compromettere il corretto funzionamento del servizio, inclusi atti di
                  hacking, distribuzione di virus informatici o qualsiasi altra attività lesiva per la
                  sicurezza del servizio stesso o dei suoi utenti.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">4. Proprietà Intellettuale</h4>
                <p>
                  Il servizio e tutti i suoi contenuti, inclusi testi, grafica, loghi, immagini, video,
                  software e altri materiali, sono di proprietà del MuzNet o dei suoi licenziatari e sono
                  protetti dalle leggi sulla proprietà intellettuale. L'Utente si impegna a non utilizzare,
                  copiare, riprodurre o distribuire i contenuti del social senza l'autorizzazione scritta del
                  titolare dei diritti.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">5. Contenuti dell'Utente</h4>
                <p>
                  L'Utente mantiene la proprietà dei contenuti che carica o condivide attraverso MuzNet, ma
                  concede ad esso una licenza non esclusiva, mondiale, gratuita e trasferibile per l'utilizzo,
                  la distribuzione, la riproduzione, la modifica e la visualizzazione di tali contenuti nel
                  contesto del social. L'Utente si impegna a non caricare o condividere contenuti che violino
                  i diritti di terzi o che siano diffamatori, offensivi o illegali.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">6. Privacy</h4>
                <p>
                  La privacy dell'Utente è importante per MuzNet. L'Utente accetta di consentire la raccolta,
                  l'elaborazione e l'utilizzo dei propri dati personali in conformità con la Politica sulla
                  Privacy del social, che può essere consultata separatamente.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">7. Limitazione di responsabilità</h4>
                <p>
                  Il servizio non può garantire l'accuratezza, la completezza o la sicurezza dei contenuti
                  presenti in esso o generati dagli Utenti. L'Utente utilizza MuzNet a proprio rischio e il
                  social declina ogni responsabilità per eventuali danni derivanti dal suo uso.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">8. Modifiche ai Termini</h4>
                <p>
                  Il servizio si riserva il diritto di apportare modifiche ai presenti Termini in qualsiasi
                  momento. L'Utente sarà informato delle modifiche attraverso MuzNet stesso. L'utilizzo
                  del social da parte dell'Utente dopo la pubblicazione delle modifiche implica
                  l'accettazione delle stesse.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">9. Risoluzione del Contratto</h4>
                <p>
                  L'Utente può interrompere il proprio utilizzo del servizio in qualsiasi momento. MuzNet può
                  sospendere o terminare l'account dell'Utente in caso di violazione dei presenti Termini o
                  per motivi di sicurezza o manutenzione del social.
                </p>
              </li>
              <li class="text-left">
                <h4 class="mt-2 text-lg font-semibold">10.</h4>
                <p>
                  Legge applicabile e giurisdizione I presenti Termini sono disciplinati dalle leggi dello
                  Stato italiano. Ogni controversia derivante dai presenti Termini sarà sottoposta alla
                  giurisdizione esclusiva dei tribunali competenti di Lecco.
                  Si prega di leggere attentamente i presenti Termini e di contattarci in caso di domande o dubbi.
                </p>
              </li>
            </ul>
            </p>
          </div>
          <!-- Footer -->
          <div class="flex items-center p-6 space-x-2 border-t border-gray-200 rounded-b dark:border-gray-600">
            <button data-modal-hide="terms-modal" type="button"
              class="text-white bg-violet-600 hover:bg-violet-700 focus:ring-4 focus:outline-none focus:ring-violet-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-violet-700 dark:hover:bg-violet-700 dark:focus:ring-violet-800">
              Accetta
            </button>
            <button data-modal-hide="terms-modal" type="button"
              class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-violet-300 rounded-lg border border-gray-200 text-sm font-medium px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-violet-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
              Rifiuta
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useDark } from "@vueuse/core";

const isDark = useDark();
</script>
