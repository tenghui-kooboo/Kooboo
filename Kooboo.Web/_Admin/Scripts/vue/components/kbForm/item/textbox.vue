<template>
  <input
  type="text"
  v-model="fieldValue"
  class="form-control"
  :placeholder="placeholder"
/>

</template>
<script>
import formItemMixin from '../mixin/mixins'
import fieldValidateMixin from '../mixin/validate'
export default {
   props: {
        data: String,
        name: String,
        rules: Array,
        options: Object|Array,
        ctx: Object,
        placeholder: String
      },

      mixins: [fieldValidateMixin,formItemMixin],
      
}
</script>

