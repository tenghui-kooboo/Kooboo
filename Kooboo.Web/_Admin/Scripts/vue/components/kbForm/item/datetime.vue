<template>
  <input type="datetime-local" v-model="fieldValue" class="form-control" :placeholder="placeholder">
</template>
<script>
import formItemMixin from '../mixin/mixins'
import fieldValidateMixin from '../mixin/validate'

export default {
  props: {
    data: String,
    name: String,
    placeholder: String
  },

  mixins: [fieldValidateMixin, formItemMixin],
}
</script>
