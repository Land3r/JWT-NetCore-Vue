<template>
  <q-layout view="hHh lpR fFf" class="bg-space">

    <q-page-container>
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script>
export default {
  name: 'EmptyLayout',
  data () {
    return {
    }
  }
}
</script>

<style>
.bg-space {
  background-image: url("~assets/space.jpg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
}
</style>
