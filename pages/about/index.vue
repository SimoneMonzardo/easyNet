
<template>
    <h1>{{patate}}</h1>
</template>
  

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  name: 'IndexPage',
  data:function(){
    return{
        patate:12,
        firstname:12,
    }
  },
  computed:{
    fullname:function(){
        return this.firstname
    }
  }
})
</script>
