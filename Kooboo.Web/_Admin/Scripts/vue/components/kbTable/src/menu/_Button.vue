<template>
  <inner-button
    :meta="meta"
    :bind-url="bindUrl"
    @click="onClick"
    :icon="icon"
    :class="[ 
      'btn navbar-btn',
      meta.class,
      { 'pull-right': alignRight },
      { 'btn-default': icon }
    ]"
  />
</template>

<script>
import InnerButton from '../components/_Button'
import actionMixin from '../actionMixin'

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

  computed: {
    alignRight() {
      return this.meta.align === 'right' || this.meta.align == 1
    }
  },

  mixins: [actionMixin],

  methods: {
    bindUrl(url) {
      return this.$parameterBinder.bind(url)
    },

    onClick() {
      switch (this.actionType) {
        case 'post':
          if (this.action.confirm) {
            var text = eval("Kooboo.text." + this.action.confirm)
            if (confirm(text)) {
              api.post(this.bindUrl(this.action.url), { ids: this.selected.map(o => o.id) })
            }
          } else {
            api.post(this.bindUrl(this.action.url), { ids: this.selected.map(o => o.id) })
          }

          break;
        case 'event':
          this.$root[this.action.url](this.selected)
          break;
        case 'popup':
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(this.action.url),
            context: this.list,
            selected: this.selected,
            meta: this.actionMeta
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
