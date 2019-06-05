<template>
  <a ref="button" :href="href" :target="target">
    <slot>
      <i v-if="icon" :class="`fa fa-${meta.iconClass}`"></i>
      <template v-else>{{ meta.text }}</template>
    </slot>
  </a>
</template>

<script>
import actionMixin from '../actionMixin'
export default {
  name: 'InnerButton',

  props: {
    meta: Object,
    bindUrl: Function,
    icon: {
      type: Boolean,
      default: false
    }
  },

  mixins:[actionMixin],

  mounted () {
    if (this.isAction) {
      this.$refs.button.addEventListener('click', this.onClick)
    }
  },

  computed: {
    href () {
      if (this.isAction) {
        return 'javascript:;'
      } else {
        return this.bindUrl(this.action.url)
      }
    },

    target () {
      if (this.actionType === 'newWindow') {
        return '_blank'
      } else {
        return null
      }
    }
  },

  methods: {
    onClick (e) {
      e.preventDefault()
      this.$emit('click', e)
    }
  }
}
</script>