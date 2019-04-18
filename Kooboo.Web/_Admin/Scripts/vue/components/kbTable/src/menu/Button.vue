<template>
  <inner-button :meta="meta" :selected="selected" />
</template>

<script>
import InnerButton from './_Button'

export default {
  name: 'MenuButton',

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
