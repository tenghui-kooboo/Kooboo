<template>
  <td>{{ text }}</td>
</template>

<script>
import Mixins from './mixins'

export default {
  name: 'CellText',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

  mixins: [ Mixins ]
}
</script>
