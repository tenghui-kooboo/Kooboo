<template>
  <select class="form-control" v-model="fieldValue">
    <option :key="key" :value="opt.value" v-for="(opt,key) in finalOptions">
      {{
      opt.displayName
      }}
    </option>
  </select>
</template>
<script>
import formItemMixin from '../mixin/mixins'
import fieldValidateMixin from '../mixin/validate'

export default {
  props: {
    data: String,
    name: String,
    options: Object | Array,
    placeholder: String,
    ctx: Object
  },

  data() {
    return {
      fieldValue: this.data
    };
  },

  mixins: [fieldValidateMixin, formItemMixin],

  computed: {
    finalOptions() {
      var finalOptions = [];

      if (this.kbitem.placeholder && this.kbitem.defaultValue && this.kbitem.defaultValue.value) {
        finalOptions.push({
          displayName: this.kbitem.placeholder,
          value: this.kbitem.defaultValue.value
        });
      }

      if (this.options instanceof Array) {
        finalOptions = finalOptions.concat(this.options);
      } else if (this.options instanceof Object) {
        var self = this;
        finalOptions = finalOptions.concat(
          this.ctx[this.options.data].map(function (item) {
            return {
              displayName: self
                .$parameterBinder
                .formatText(self.options.text, item),
              value: self
                .$parameterBinder
                .formatText(self.options.value, item)
            };
          })
        );
      }

      return finalOptions;
    }
  },
}
</script>

