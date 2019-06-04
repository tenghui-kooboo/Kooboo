<template>
  <div class="navbar navbar-default" v-if="meta&&meta.length>0">
    <div class="container-fluid">
      <component v-for="(item, index) in meta" :key="index"
        :is="`menu-${item.type}`" v-show="visible(item)"
        :meta="item" :data="data" :selected="selected" :list="list" />
    </div>
  </div>
</template>

<script>
import MenuComponents from './menu/index'

export default {
  name: "TableMenu",

  props: {
    meta: Array,
    selected: Array,
    data: Object|Array,
    list: Array
  },

  methods: {
    visible (item) {
      if (!item.visible) {
        return true
      }

      const compare = item.visible.compare === '=' ? '===' : item.visible.compare
      return eval(`${this.selected.length} ${compare} ${item.visible.value}`)
    }
  },

  components: {
    ...MenuComponents
  }
};
</script>