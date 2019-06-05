<template>
  <td class="table-action">
    <inner-button :meta="meta" :row="row" :name="name" :list="list"
      :class="[ 'btn btn-sm', meta.class ]" :style="meta.style">
      {{ meta.text }}
    </inner-button>
  </td>
</template>

<script>
import Mixins from './mixins'
import InnerButton from './_Button'

export default {
  name: 'CellButton',

  props: {
    name: String,
    meta: Object,
    list: Array,
    row: Object
  },

  mixins: [ Mixins ],

  components: {
    InnerButton
  }
}
</script>
