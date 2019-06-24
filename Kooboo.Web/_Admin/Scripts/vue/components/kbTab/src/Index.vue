<template>
  <div>
    <tab :tabs="meta.tabs"></tab>
    <kb-table :meta="innermeta"></kb-table>
  </div>
</template>
<script>
import Tab from './Tab.vue'
export default {
  props: {
    meta: Object
  },

  data() {
    return {
      innermeta: this.getmeta(this.meta.tabs[0])
    }
  },
  provide() {
    return {
      ktab: this
    }
  },

  computed: {
    selectTab: {
      get() {
        return this.meta.tabs[0]
      },
      set(tab) {
        this.innermeta = this.getmeta(tab)
      }
    }
  },
  methods: {
    getmeta(tab) {
      var meta = tab.view;

      if (!meta.dataApi) {
        meta.dataApi = tab.dataApi
      }
      if (!meta.modelType) {
        meta.modelType = this.meta.modelType
      }

      return meta
    }
  },
  components: {
    Tab,
  }
}
</script>
