<template>
  <div>
    <Header
      :loggedIn="logged"
      :email="email"
      :userName="username"
      :profilePicture="profilePicture"
    />
    <div class="min-h-[calc(100vh-4rem)] flex justify-center mt-16">
      <slot />
    </div>
    <Footer />
  </div>
</template>
<script>
export default {
  data: () => ({
    logged: false,
    username: "",
    email: "",
    profilePicture:
      "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png",
  }),
  mounted: function () {
    this?.$nextTick(function () {
      this.getLocalStorage();
    });
    window.addEventListener("load", this.getLocalStorage());
  },
  methods: {
    getLocalStorage() {
      const loggedSessionCache = sessionStorage.getItem("logged");
      if (loggedSessionCache !== null && loggedSessionCache === "true") {
        this.logged = true;
      } else {
        const loggedBrowserCache = localStorage.getItem("logged");

        this.logged = loggedBrowserCache !== null && loggedBrowserCache === "true";
        sessionStorage.setItem("logged", this.logged);
      }

      const usernameSessionCache = sessionStorage.getItem("username");
      if (usernameSessionCache !== null && usernameSessionCache !== "undefined") {
        this.username = usernameSessionCache;
      } else {
        const usernameBrowserCache = localStorage.getItem("username");

        this.username = usernameBrowserCache !== null && usernameBrowserCache !== "undefined"
          ? usernameBrowserCache
          : '';
        sessionStorage.setItem("username", this.username);
      }

      const emailSessionCache = sessionStorage.getItem("email");
      if (emailSessionCache !== null && emailSessionCache !== "undefined") {
        this.email = emailSessionCache;
      } else {
        const emailBrowserCache = localStorage.getItem("email");

        this.email = emailBrowserCache !== null && emailBrowserCache !== "undefined"
          ? emailBrowserCache
          : '';
        sessionStorage.setItem("email", this.email);
      }

      const pictureSessionCache = sessionStorage.getItem("profilePicture");
      if (pictureSessionCache !== null && pictureSessionCache !== "undefined") {
        this.profilePicture = pictureSessionCache;
      } else {
        const pictureBrowserCache = localStorage.getItem("profilePicture");

        this.profilePicture = pictureBrowserCache !== null && pictureBrowserCache !== "undefined"
          ? pictureBrowserCache
          : '';
        sessionStorage.setItem("profilePicture", this.profilePicture);
      }

      const tokenSessionStorage = sessionStorage.getItem('token');
      if (tokenSessionStorage === null  || tokenSessionStorage === "undefined") {
        sessionStorage.setItem('token', localStorage.getItem('token'));
      }
    },
  },
};
</script>
