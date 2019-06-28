<template>
  <div>
    <label :key="index" v-for="(opt,index) in options" class="checkbox-inline">
      <input type="checkbox" v-model="fieldValue" :value="opt.data">
      {{
      opt.text
      }}
    </label>
  </div>
</template>
<script>
import formItemMixin from '../mixin/mixins'
import fieldValidateMixin from '../mixin/validate'
export default {
  props: {
    data: String,
    name: String,
    options: Array | Object,
    placeholder: String
  },

  mixins: [fieldValidateMixin, formItemMixin],
}
</script>

