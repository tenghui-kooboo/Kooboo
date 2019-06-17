<template>
  <div>
    <inner-menu
      v-if="meta_d"
      :modeltype="meta_d.modelType"
      :meta="meta_d.menu"
      :data="typeof data_d === 'object' ? data_d : null"
      :list="list"
      :selected="selected"
    ></inner-menu>
    <inner-table v-if="meta_d" :ctx="ctx" :meta="meta_d" :list="list" :showSelected="showSelected"></inner-table>
  </div>
</template>

<script>
import InnerMenu from "./Menu.vue";
import InnerTable from "./Table.vue";

export default {
  name: "KoobooTable",

  props: {
    metaName: String,
    meta: Object,
    data: Object,
    listName: String,
    ctx: Object,
    view: Object
  },
  data() {
    return {
      meta_d: this.meta,
      data_d: this.data
    }
  },
  created() {
    if (this.metaName) {
      this.meta_d = api.tableMeta(this.metaName);
    } 
    
    var self = this;
    if (this.meta_d.dataApi) {
      var parameters = (this.ctx && this.ctx.parameters) ? this.ctx.parameters : {};
      api
        .get(this.$parameterBinder.bind(this.meta_d.dataApi, parameters))
        .then(function (res) {
          if (res.success) {
            self.data_d = res.model;
          }
        });
    }
  },
  computed: {
    showSelected() {
      if (this.meta_d.hasOwnProperty("showSelected")) {
        return this.meta_d.showSelected ? true : false;
      }
      //default is show
      return true;
    },
    selected() {
      return this.list.filter(o => o.__selected)
    },

    list() {
      if (!this.data_d) {
        return []
      } else {
        return this.listName ? this.data_d[this.listName] : this.data_d
      }
    }
  },

  components: {
    InnerMenu,
    InnerTable
  }
};
</script>
