<template>
  <div>
    <label :key="index" v-for="(opt,index) in options" class="radio-inline">
      <input type="radio" name="group" :value="opt.data" v-model="fieldValue">
      {{ opt.text }}
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

