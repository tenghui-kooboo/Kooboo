<template>
  <td>
    <a
      v-for="(value, key) in row[name]"
      :key="key"
      @click="onClick(key)"
      class="label label-sm kb-table-label-refer"
      :style="`background-color:${colorClass(key)}`"
    >{{ labelText(key, value) }}</a>
  </td>
</template>

<script>
import Mixins from './mixins'
import actionMixin from '../actionMixin'

export default {
  name: 'CellArray',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

  inject:['table'],

  methods: {
    colorClass(key) {
      return Kooboo.getLabelColor(key)
    },

    labelText(key, value) {
      if (!this.meta.text) {
        return value
      } else {
        const template = this.meta.text.replace('{key}', `{component.table.${key}\}`)
        return this.formatText(template, value)
      }
    },
    onClick(key) {
      switch (this.actionType) {
        case "popup":
          var parameters = this.getParameters(key, this.table.meta.modelType);

          this.$root.$refs.popup.show({
            parameters: parameters,
            context: this.list,
            selected: this.selected,
            meta: this.actionMeta
          })
          break;
      }
    },
    getParameters(key, type) {
      var parameters = {};
      if (this.action.url) {
        parameters = this.$parameterBinder.getKeyValue(this.action.url, this.row)
      }
      parameters = Vue.util.extend(parameters, this.row)
      parameters = Vue.util.extend(parameters, { by: key, type: type })
      return parameters
    }
  },

  mixins: [Mixins, actionMixin]
}
</script>
