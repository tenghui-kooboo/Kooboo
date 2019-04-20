<template>
  <td class="table-action">
    <inner-button :meta="meta" :row="row" :name="name" :icon="true" :list="list"
      :class="[ 'btn btn-default', meta.class ]" :style="meta.style" />
  </td>
</template>

<script>
import Mixins from './mixins'
import InnerButton from './_Button'

export default {
  name: 'CellIcon',

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
