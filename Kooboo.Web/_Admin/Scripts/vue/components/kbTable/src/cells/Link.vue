<template>
  <td>
    <inner-button :meta="meta" :row="row" :name="name"
      :class="meta.class" :style="`cursor:pointer; ${meta.style}`">{{ text }}</inner-button>
  </td>
</template>

<script>
import Mixins from './mixins'
import InnerButton from './_Button'

export default {
  name: 'CellLink',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

  mixins: [ Mixins ],

  components: {
    InnerButton
  }
}
</script>
