<template>
  <td>{{ text }}</td>
</template>

<script>
import Mixins from './mixins'

export default {
  name: 'CellDate',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

  computed: {
    text () {
      
      const date = new Date(this.row[this.name])
  
      return date.toDefaultLangString()
    }
  },

  mixins: [ Mixins ]
}
</script>
