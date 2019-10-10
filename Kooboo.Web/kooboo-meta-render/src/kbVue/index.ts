import Vue from "vue";
import { context, componentPool } from "@/kbVue/global";
import { getQuery } from "@/common/utils";
import { getData } from "@/common/api";
import { kInstance } from "@/kbVue/global";

export default Vue.extend({
  props: {
    meta: Object
  },
  async mounted() {
    if (this.meta.hooks && this.meta.hooks instanceof Array) {
      for (const i of this.meta.hooks) {
        context.$on(i.name.toLowerCase(), (k: any) => {
          k.self = this;
          k.pool = componentPool;
          eval(i.execute);
        });
      }
    }
    console.log("mounted", this.meta.id);
    componentPool[this.meta.id] = this;
    this.$dispath("load");
  },
  destroyed() {
    if (this.meta.hooks && this.meta.hooks instanceof Array) {
      for (const i of this.meta.hooks) {
        context.$off(i.name.toLowerCase());
      }
    }

    console.log("destroyed", this.meta.id);
    delete componentPool[this.meta.id];
    this.$dispath("remove");
  },
  methods: {
    $dispath: function(hookType: string, k: any = {}) {
      for (const key in kInstance) {
        k[key] = kInstance[key];
      }
      k.target = this;
      k.query = getQuery();
      context.$emit(`${hookType}_${this.meta.id}`.toLowerCase(), k);
    },
    async $loadData(url: string, name: string = "data") {
      let data = await getData(url);
      (this as any)[name] = data;
      this.$dispath("dataLoad", {
        data: data
      });
    }
  }
});
