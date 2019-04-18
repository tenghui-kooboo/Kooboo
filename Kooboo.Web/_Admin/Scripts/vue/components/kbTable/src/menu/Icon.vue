<template>
  <inner-button :meta="meta" :selected="selected" :icon="true" />
</template>

<script>
import InnerButton from './_Button'

export default {
  name: 'MenuIcon',

  props: {
    meta: Object,
    selected: Array,
    data: Object
  },

  components: {
    InnerButton
  }
}
</script>
