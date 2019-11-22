<template>
  <q-card class="app-transparent-85 app-popout" transition-show="slide-down" transition-hide="slide-up">
    <slot></slot>
  </q-card>
</template>

<script>
export default {
  name: 'PublicCard'
}
</script>
