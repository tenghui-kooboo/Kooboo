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
        var model = {}
        if (tab.key) {
          model[tab.key] = tab.value
        }

        meta.dataApi = this.$parameterBinder.bind(tab.dataApi, model)
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
