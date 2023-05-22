<template>
<div>
  <NavBar/>
  <SettingSelector/>
</div>
</template>

<script>
import NavBar from '~/components/NavBar.vue';
import SettingSelector from '~/components/SettingSelector.vue';

export default {
    name: "IndexPage",
    components: { NavBar,SettingSelector }
}
</script>
