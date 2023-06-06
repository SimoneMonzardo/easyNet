<template>
  <div
    id="register-modal"
    tabindex="-1"
    aria-hidden="true"
    class="pulseAnim bg-gray-900 bg-opacity-50 fixed top-0 left-0 right-0 z-50 hidden w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-full max-h-full"
  >
    <div class="relative w-full max-w-3xl max-h-full">
      <!-- Modal content -->
      <div
        class="relative bg-white rounded-lg shadow dark:bg-gray-700 flex flex-row"
      >
        <div
          class="bg-gray-200 dark:bg-gray-800 flex-col content-center rounded-l-lg w-64 p-5 text-gray-900 justify-evenly hidden sm:flex"
        >
          <h4 class="text-2xl font-semibold mx-auto text-violet-600">
            Benvenuto
          </h4>
          <h6 class="mx-auto text-4xl font-semibold text-violet-600">MuzNet</h6>
          <img
            src="~/public/logo.png"
            class="mt-3 h-15 rounded-full"
            alt="MuzNet Logo"
          />
        </div>
        <div class="w-full">
          <button
            type="button"
            class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
            data-modal-hide="register-modal"
          >
            <svg
              aria-hidden="true"
              class="w-5 h-5"
              fill="currentColor"
              viewBox="0 0 20 20"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fill-rule="evenodd"
                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                clip-rule="evenodd"
              ></path>
            </svg>
            <span class="sr-only">Chiudi</span>
          </button>
          <div class="px-6 py-6 lg:px-8">
            <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">
              Registrati a MuzNet
            </h3>
            <form
              class="space-y-6"
              action="#"
              method="post"
              enctype="multipart/form-data"
            >
              <div class="grid gap-4 mb-4 sm:grid-cols-2 sm:gap-6 sm:mb-5">
                <div class="relative">
                  <input
                    autocomplete="off"
                    type="text"
                    id="name"
                    name="name"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="name"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Nome</label
                  >
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="text"
                    id="surname"
                    name="surname"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="surname"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Cognome</label
                  >
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="tel"
                    id="phoneNumber"
                    name="phoneNumber"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="phoneNumber"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Numero di telefono</label
                  >
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="date"
                    id="birthDate"
                    name="dateOfBirth"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="birthDate"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Data di nascita</label
                  >
                </div>

                <div>
                  <select
                    id="gender"
                    required
                    name="gender"
                    class="px-2.5 pb-2.5 pt-4 bg-white border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-violet-600 focus:border-violet-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white dark:focus:ring-violet-600 dark:focus:border-violet-600"
                  >
                    <option
                      selected
                      disabled
                      class="disabled:text-gray-500 dark:disabled:text-gray-400"
                    >
                      Inserisci il genere
                    </option>
                    <option value="male">Maschio</option>
                    <option value="female">Femmina</option>
                  </select>
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="email"
                    id="email"
                    name="email"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="email"
                    id="emailText"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Email</label
                  >
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="password"
                    id="passwordRegister"
                    name="password"
                    v-on:keyup="check()"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="passwordRegister"
                    id="passwordText"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Password</label
                  >
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="password"
                    id="confirmPassword"
                    v-on:keyup="check()"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="confirmPassword"
                    id="confirmText"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Conferma Password</label
                  >
                </div>

                <div class="relative">
                  <input
                    autocomplete="off"
                    type="text"
                    id="usernameRegister"
                    name="username"
                    class="block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border border-gray-300 appearance-none dark:text-white dark:border-gray-500 dark:focus:border-violet-600 focus:outline-none focus:ring-0 focus:border-violet-700 peer"
                    placeholder=" "
                    required
                  />
                  <label
                    for="usernameRegister"
                    id="usernameText"
                    class="absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-700 px-2 peer-focus:px-2 peer-focus:text-violet-700 peer-focus:dark:text-violet-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
                    >Username</label
                  >
                </div>

                <button
                  type="submit"
                  @click="handleRegister()"
                  class="w-full text-white bg-violet-600 hover:bg-violet-700 focus:ring-4 focus:outline-none focus:ring-violet-300 font-medium rounded-xl text-sm px-5 py-2.5s p text-center dark:bg-violet-700 dark:hover:bg-violet-700 dark:focus:ring-violet-800"
                >
                  Crea Account
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import useAuth from "~/composables/useAuth";

export default {
  name: "RegisterPopup",
  methods: {
    async api(data) {
      await useFetch(
        "https://progettoeasynet.azurewebsites.net/Auth/Register",
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
          },
          method: "POST",
          body: JSON.stringify(data),
          onRequest({ request, options }) {
            // Set the request headers
          },
          onRequestError({ request, options, error }) {
            // Handle the request errors
          },
          onResponse({ request, response, options }) {
            console.log(response._data);
            if (response._data.result == "User created successfully") {
              const options = {};
              console.log("success");

              const registerElement = document.getElementById("register-modal");
              const registerModal = new Modal(registerElement, options);
              registerModal.hide();
              try {
                const successElement = document.getElementById("success-modal");
                const successModal = new Modal(successElement, options);
                successModal.show();
              } catch (error) {
                consol.log(error);
              }
            }
            if (response._data == "Mail already used") {
              console.log(document.getElementById("emailText").innerHTML);
              try {
                document.getElementById("emailText").innerHTML =
                  "Email già in uso";
                document.getElementById("email").style.borderColor = "red";
                document.getElementById("emailText").style.color = "red";
              } catch (error) {
                console.log(error);
              }
            } else if (response._data != "Mail already used") {
              document.getElementById("emailText").innerHTML = "Email";
              document.getElementById("email").style.borderColor =
                "rgba(80,80,80,0.3)";
              document.getElementById("emailText").style.color = "gray";
            }
            if (response._data == "Username already used") {
              try {
                document.getElementById("usernameText").innerHTML =
                  "Username già in uso";
                document.getElementById("usernameRegister").style.borderColor =
                  "red";
                document.getElementById("usernameText").style.color = "red";
              } catch (error) {
                console.log(error);
              }
            } else if (response._data != "Username already used") {
              document.getElementById("usernameText").innerHTML = "Username";
              document.getElementById("usernameRegister").style.borderColor =
                "rgba(80,80,80,0.3)";
              document.getElementById("usernameText").style.color = "gray";
            }
          },
          onResponseError({ request, response, options }) {
            // Handle the response errors
          },
        }
      );
    },
    check() {
      var re = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{6,}$/;
      const password = document.getElementById("passwordRegister").value;
      const confirmPassword = document.getElementById("confirmPassword").value;
      if (password === confirmPassword && password != "") {
        document.getElementById("confirmText").innerHTML = "Conferma password";
        document.getElementById("confirmPassword").style.borderColor = "green";
        document.getElementById("confirmText").style.color = "green";

        document.getElementById("passwordText").innerHTML = "Password valida";
        document.getElementById("passwordRegister").style.borderColor = "green";
        document.getElementById("passwordText").style.color = "green";
      }
      if (!re.test(password)) {
        document.getElementById("passwordText").innerHTML =
          "Password non valida";
        document.getElementById("passwordRegister").style.borderColor = "red";
        document.getElementById("passwordText").style.color = "red";
        if (password === "") {
          console.log("entrato");

          document.getElementById("confirmText").innerHTML =
            "Conferma Password";
          document.getElementById("confirmPassword").style.borderColor =
            "rgba(80,80,80,0.3)";
          document.getElementById("confirmText").style.color = "gray";

          document.getElementById("passwordText").innerHTML = "Password";
          document.getElementById("passwordRegister").style.borderColor =
            "rgba(80,80,80,0.3)";
          document.getElementById("passwordText").style.color = "gray";
        }
        document.getElementById("confirmText").innerHTML = "Conferma Password";
        document.getElementById("confirmPassword").style.borderColor =
          "rgba(80,80,80,0.3)";
        document.getElementById("confirmText").style.color = "gray";
      } else {
        document.getElementById("passwordText").innerHTML = "Password valida";
        document.getElementById("passwordRegister").style.borderColor = "green";
        document.getElementById("passwordText").style.color = "green";
        if (password != confirmPassword) {
          document.getElementById("confirmText").innerHTML = "Non coincidono";
          document.getElementById("confirmPassword").style.borderColor = "red";
          document.getElementById("confirmText").style.color = "red";
        }
      }
    },
    async handleRegister() {
      //const { register } = useAuth();
      const passwordInput = document.getElementById("passwordRegister");
      const confirmPasswordInput = document.getElementById("confirmPassword");
      var data = {
        username: document.getElementById("usernameRegister").value,
        name: document.getElementById("name").value,
        surname: document.getElementById("surname").value,
        phoneNumber: document.getElementById("phoneNumber").value,
        email: document.getElementById("email").value,
        gender: document.getElementById("gender").value,
        dateOfBirth: document.getElementById("birthDate").value,
        password: passwordInput.value,
      };

      passwordInput.value = "";
      confirmPasswordInput.value = "";

      await this.api(data);
    },
  },
};
</script>

<style>
.pulseAnim {
  animation: pulse 0.1s cubic-bezier(0.4, 0, 0.6, 1);
}
@keyframes pulse {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
</style>
