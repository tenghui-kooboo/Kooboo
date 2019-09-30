import Vue from "vue";
import { context, states } from "@/common/global";
import { getQuery } from "@/common/utils";

export default Vue.extend({
  props: {
    meta: Object
  },
  async mounted() {
    states[this.meta.id] = this.$data;
    this.$dispath("load");
  },
  destroyed() {
    delete states[this.meta.id];
  },
  created() {
    if (this.meta.hooks && this.meta.hooks instanceof Array) {
      for (const i of this.meta.hooks) {
        context.$on(i.name, (k: any) => {
          k.target = this.$el;
          k.state = states;
          k.me = this;
          eval(i.execute);
        });
      }
    }
  },
  methods: {
    $dispath: function(hookType: string, k: any = {}) {
      k.self = this.$el;
      k.query = getQuery();
      context.$emit(`${hookType}_${this.meta.id}`, k);
    }
  }
});
