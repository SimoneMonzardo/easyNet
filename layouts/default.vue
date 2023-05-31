<template>
  <div>
    <Header
      :loggedIn="logged"
      :email="email"
      :userName="username"
      :profilePicture="profilePicture"
    />
    <div class="min-h-[calc(100vh-4rem)] flex flex-row">
      <div class="flex justify-center h-screen">
        <slot />
      </div>
      <Sidebar />
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
      this.logged = localStorage.getItem("logged") === "true";
      if (this.logged === null) {
        this.logged = false;
      }

      this.username = localStorage.getItem("username");
      if (this.username === null) {
        this.username = "";
      }

      this.email = localStorage.getItem("email");
      if (this.email === null) {
        this.email = "";
      }

      this.profilePicture = localStorage.getItem("profilePicture");
      if (this.profilePicture === null || this.profilePicture === "undefined") {
        this.profilePicture =
          "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
      }
    },
  },
};
</script>
