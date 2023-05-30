<template>
  <div>
    <Header
      email="temp@mail.com"
      userName="Temp User"
      profilePicture="https://st2.depositphotos.com/1007995/8863/i/600/depositphotos_88637478-stock-photo-side-profile-picture-of-a.jpg"
    />
    <div class="min-h-[calc(100vh-4rem)] flex flex-row">
      <div class="flex justify-center h-screen mx-20">
        <slot />
      </div>
      <Sidebar />
    </div>
    <Footer />
  </div>
</template>
