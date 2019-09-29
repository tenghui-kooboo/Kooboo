<script lang="ts">
import Vue from "vue";
import { context, providers } from "@/common/global";
export default Vue.extend({
  props: {
    meta: Object
  },
  mounted() {
    providers[this.meta.id] = this.$data;
  },
  destroyed() {
    console.log("destroyed")
    delete providers[this.meta.id];
  },
  created() {
    if (this.meta.hooks && this.meta.hooks instanceof Array) {
      for (const i of this.meta.hooks) {
        context.$on(i.name, (k: any) => {
          k.target = this.$el;
          k.provider = providers;
          eval(i.action);
        });
      }
    }
  }
});
</script>