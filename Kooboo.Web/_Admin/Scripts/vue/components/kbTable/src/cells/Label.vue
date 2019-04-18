<template>
  <td>
    <span v-show="visible"
      :class="`label ${this.class}`" :style="meta.style">{{ text }}</span>
  </td>
</template>

<script>
import Mixins from './mixins'

export default {
  name: 'CellLabel',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

  mixins: [ Mixins ]
}
</script>
