<template>
  <inner-button :meta="meta" :bind-url="bindUrl" :icon="icon" @click="onClick">
    <slot></slot>
  </inner-button>
</template>

<script>
import InnerButton from '../components/_Button'
import actionMixin from '../actionMixin'

export default {
  name: 'CellInnerButton',

  props: {
    name: String,
    meta: Object,
    list: Array,
    row: Object,
    icon: {
      type: Boolean,
      default: false
    }
  },

  inject: ['table'],
  
  mixins:[actionMixin],

  methods: {
    bindUrl(url) {
      url = url || this.row[this.name]
      var data = this.row
      if (this.table.ctx && this.table.ctx.parameters) {
        data = Vue.util.extend({}, this.row)
        data = Vue.util.extend(data, this.table.ctx.parameters)
      }
      url = this.$parameterBinder.bind(url, data)
      return url
    },

    onClick() {
      switch (this.actionType) {
        case 'event':
          this.$root[this.action.url](this.row)
          break;
        case 'post':
          api.post(this.bindUrl(this.action.url, this.row))
          break;
        case "popup":
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(this.action.url),
            context: this.list,
            row: this.row
          })
          break;
      }
    }
  },

  components: {
    InnerButton
  }
}
</script>
