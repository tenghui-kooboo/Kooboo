<template>
  <inner-button :meta="meta" :selected="selected" :list="list" />
</template>

<script>
import InnerButton from './_Button'

export default {
  name: 'MenuButton',

  props: {
    meta: Object,
    selected: Array,
    data: Object,
    list: Array
  },

  components: {
    InnerButton
  }
}
</script>
