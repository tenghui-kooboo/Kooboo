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
  
  computed:{
    operator(){
      if (!this.item.visible) {
        return null
      }
      if(this.item.visible.compare){
        return this.item.visible.compare
      }
      return this.item.visible.operator
    }
  },

  methods: {
    visible (item) {
      if (!item.visible) {
        return true
      }
      var operator=this.getOperator(item)
      const compare = operator === '=' ? '===' : operator
      return eval(`${this.selected.length} ${compare} ${item.visible.value}`)
    },
    //for adapter old field
    getOperator(item){
      if (!item.visible) {
        return null
      }
      if(item.visible.compare){
        return item.visible.compare
      }
      return item.visible.operator
    }
  },

  components: {
    ...MenuComponents
  }
};
</script>