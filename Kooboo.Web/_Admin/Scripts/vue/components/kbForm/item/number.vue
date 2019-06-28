<template>
  <input type="number" v-model="fieldValue" class="form-control" :placeholder="placeholder">
</template>
<script>

import formItemMixin from '../mixin/mixins'
import fieldValidateMixin from '../mixin/validate'

export default {
  props: {
    data: String,
    options: Object | Array,
    name: String,
    ctx: Object,
    placeholder: String
  },

  mixins: [fieldValidateMixin, formItemMixin],
}
</script>
