<template>
  <inner-button :meta="meta" :selected="selected" :icon="true" :list="list" />
</template>

<script>
import InnerButton from './_Button'

export default {
  name: 'MenuIcon',

  props: {
    meta: Object,
    selected: Array,
    data: Object|Array,
    list: Array
  },

  components: {
    InnerButton
  }
}
</script>
