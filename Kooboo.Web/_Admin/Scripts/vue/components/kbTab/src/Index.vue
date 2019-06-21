<template>
  <div>
    <tab :tabs="tabs"></tab>
    <kb-table :meta="meta"></kb-table>
  </div>
</template>
<script>
import Tab from './Tab.vue'
export default {
  props: {
    tabs: Array
  },

  // computed:{
  //   selectView:{
  //     get(){
  //       return this.view
  //     },
  //     set(meta){
  //       this.$emit("update:view",meta)
  //       //this.view=meta
  //     }
  //   }
  // },
  data() {
    return {
      meta_d: null
    }
  },
  provide() {
    return {
      ktab: this
    }
  },
  computed: {
    meta: {
      get() {
        debugger
        if (!this.meta_d) {
          this.meta_d=this.getmeta(this.tabs[0])
        }
        return this.meta_d
      },
      set(tab) {
        this.meta_d=this.getmeta(tab)
      }
    }
  },
  methods: {
    getmeta(tab) {
      var meta= tab.view;

      if (!meta.dataApi) {
        meta.dataApi = tab.dataApi
      }
      
      return meta
    }
  },
  components: {
    Tab,
  }
}
</script>
