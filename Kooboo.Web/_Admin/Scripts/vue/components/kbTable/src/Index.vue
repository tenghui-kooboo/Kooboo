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
    this.meta_d = this.getmeta()
    this.data_d = this.getdata()
  },
  watch: {
    meta() {
      this.meta_d = this.getmeta()
      this.data_d=this.getdata()
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

  methods: {
    getmeta() {
      var meta = this.meta
      if (this.metaName) {
        meta = api.tableMeta(this.metaName);
      }
      return meta
    },
    getdata() {
      var self = this;
      var data = this.data
      if (this.meta_d.dataApi) {
        var parameters = (this.ctx && this.ctx.parameters) ? this.ctx.parameters : {};
        api
          .get(this.$parameterBinder.bind(this.meta_d.dataApi, parameters))
          .then(function (res) {
            if (res.success) {
              data = res.model;
            }
          });

      }
      return data
    }
  },

  components: {
    InnerMenu,
    InnerTable
  }
};
</script>
