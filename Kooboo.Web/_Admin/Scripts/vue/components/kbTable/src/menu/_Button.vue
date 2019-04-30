<template>
  <inner-button :meta="meta" :bind-url="bindUrl" @click="onClick" :icon="icon"
    :class="[ 
      'btn navbar-btn',
      meta.class,
      { 'pull-right': meta.align === 'right' },
      { 'btn-default': icon }
    ]" />
</template>

<script>
import InnerButton from '../components/_Button'

export default {
  name: 'MenuInnerButton',

  props: {
    meta: Object,
    selected: Array,
    list: Array,
    icon: {
      type: Boolean,
      default: false
    }
  },

  methods: {
    bindUrl (url) {
      return this.$parameterBinder.bind(url)
    },

    onClick () {
      switch (this.meta.action) {
        case 'post':
          api.post(this.bindUrl(this.meta.url), { ids: this.selected.map(o => o.id) })
          break;
        case 'event':
          this.$root[this.meta.url](this.selected)
          break;
        default:
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(this.meta.url),
            context: this.list,
            selected: this.selected
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
