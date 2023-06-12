<template>
  <div class="flex flex-col h-screen justify-between">
    <Header :loggedIn="data.logged" :email="data.email" :userName="data.username" :profilePicture="data.profilePicture"
      :isCompany="data.isCompany" />
    <div class="min-h-[calc(100vh-4rem)] flex justify-center mt-16">
      <slot />
    </div>
    <Footer />
  </div>
</template>

<script setup>
import { onMounted } from "vue";
import { initFlowbite } from "flowbite";
import { reactive } from "vue";

const data = reactive({
  logged: false,
  username: "",
  email: "",
  profilePicture: "",
  isCompany: false
});

const token = getToken();
console.log(token);
if (token !== null && token != 'undefined' && token !== 'null') {
  await useFetch('https://progettoeasynet.azurewebsites.net/User/IsCompany', {
    lazy: true,
    server: false,
    method: 'GET',
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Authorization': ''
    },
    onRequest({ options }) {
      options.headers['Authorization'] = `Bearer ${token}`;
    },
    onResponse({ response }) {
      try {
        data.isCompany = response.ok && response._data;
        localStorage.setItem('isCompany', data.isCompany);
        sessionStorage.setItem('isCompany', data.isCompany);
      } catch (error) { console.log(error); }
    }
  });
}

onMounted(() => {
  initFlowbite();
  window.addEventListener("load", getLocalStorage());
});

function getLocalStorage() {
  const loggedSessionCache = sessionStorage.getItem("logged");
  if (loggedSessionCache !== null && loggedSessionCache === "true") {
    data.logged = true;
  } else {
    const loggedBrowserCache = localStorage.getItem("logged");
    data.logged = loggedBrowserCache !== null && loggedBrowserCache === "true";
    sessionStorage.setItem("logged", data.logged);
  }

  const usernameSessionCache = sessionStorage.getItem("username");
  if (
    usernameSessionCache !== null &&
    usernameSessionCache !== "undefined"
  ) {
    data.username = usernameSessionCache;
  } else {
    const usernameBrowserCache = localStorage.getItem("username");

    data.username = usernameBrowserCache !== null && usernameBrowserCache !== "undefined"
      ? usernameBrowserCache
      : "";
    sessionStorage.setItem("username", data.username);
  }

  const emailSessionCache = sessionStorage.getItem("email");
  if (emailSessionCache !== null && emailSessionCache !== "undefined") {
    data.email = emailSessionCache;
  } else {
    const emailBrowserCache = localStorage.getItem("email");

    data.email =
      emailBrowserCache !== null && emailBrowserCache !== "undefined"
        ? emailBrowserCache
        : "";
    sessionStorage.setItem("email", data.email);
  }

  const pictureSessionCache = sessionStorage.getItem("profilePicture");
  if (pictureSessionCache !== null && pictureSessionCache !== "undefined") {
    data.profilePicture = pictureSessionCache;
  } else {
    const pictureBrowserCache = localStorage.getItem("profilePicture");

    data.profilePicture = pictureBrowserCache !== null && pictureBrowserCache !== "undefined"
      ? pictureBrowserCache
      : "";
    sessionStorage.setItem("profilePicture", data.profilePicture);
  }

  getToken();

  const isCompanySessionCache = sessionStorage.getItem("isCompany");
  if (isCompanySessionCache !== null && isCompanySessionCache === "true") {
    data.isCompany = true;
  } else {
    const isCompanyBrowserCache = localStorage.getItem("isCompany");
    data.isCompany = isCompanyBrowserCache !== null && isCompanyBrowserCache === "true";
    sessionStorage.setItem("isCompany", data.isCompany);
  }
}

function getToken() {
  var tokenSessionCache = sessionStorage.getItem("token");
  if (tokenSessionCache === null || tokenSessionCache === "undefined") {
    sessionStorage.setItem("token", localStorage.getItem("token"));
    tokenSessionCache = sessionStorage.getItem("token")
  }

  return tokenSessionCache;
}
</script>
