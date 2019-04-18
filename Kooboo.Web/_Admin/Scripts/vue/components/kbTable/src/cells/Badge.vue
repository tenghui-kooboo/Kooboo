<template>
  <td>
    <span v-show="visible"
      :class="`badge ${this.class}`" :style="meta.style">{{ text }}</span>
  </td>
</template>

<script>
import Mixins from './mixins'

export default {
  name: 'CellBadge',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

  mixins: [ Mixins ]
}
</script>
