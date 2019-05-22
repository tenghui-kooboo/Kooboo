<template>
  <inner-button :meta="meta" :bind-url="bindUrl" :icon="icon" @click="onClick">
    <slot></slot>
  </inner-button>
</template>

<script>
import InnerButton from '../components/_Button'

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

  methods: {
    bindUrl (url) {
      url = url || this.row[this.name]
      url = this.$parameterBinder.bind(url, this.row)
      return url
    },

    onClick () {
      switch (this.meta.action) {
        case 'event': 
          this.$root[this.meta.url](this.row)
          break;
        case 'post':
          api.post(this.bindUrl(this.meta.url, this.row))
          break;
        default:
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(this.meta.url),
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
