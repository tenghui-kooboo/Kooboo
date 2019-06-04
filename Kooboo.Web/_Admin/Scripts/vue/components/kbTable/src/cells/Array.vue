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

export default {
  name: 'CellArray',

  props: {
    name: String,
    meta: Object,
    row: Object
  },

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
      switch (this.meta.action) {
        case "popup":
          var url = this.meta.url.replace('{key}', key);
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(url,this.row),
            context: this.list,
            selected: this.selected
          })
          break;
      }
    }
  },

  mixins: [Mixins]
}
</script>
