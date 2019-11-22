<template>
  <q-slide-transition
    appear
    enter-active-class="animated flipInX"
  >
    <slot></slot>
  </q-slide-transition>
</template>

<style>
</style>

<script>
/**
 * Transition component.
 * Allow a SINGLE CHILD to use a transition on appearance.
 */
export default {
  name: 'Transition'
}
</script>
