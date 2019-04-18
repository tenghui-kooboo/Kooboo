<template>
  <a ref="button" :href="href" :target="target">
    <slot>
      <i v-if="icon" :class="`fa fa-${meta.iconClass}`"></i>
      <template v-else>{{ meta.displayName }}</template>
    </slot>
  </a>
</template>

<script>
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

  mounted () {
    if (this.isAction) {
      this.$refs.button.addEventListener('click', this.onClick)
    }
  },

  computed: {
    isAction () {
      return this.meta.action === 'post' || this.meta.action === 'event' || this.meta.action === 'popup'
    },

    href () {
      if (this.isAction) {
        return 'javascript:;'
      } else {
        return this.bindUrl(this.meta.url)
      }
    },

    target () {
      if (this.meta.action === 'newWindow') {
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