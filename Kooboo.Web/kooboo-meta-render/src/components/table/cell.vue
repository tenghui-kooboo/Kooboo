<template>
  <td>
    <component
      v-for="(item, index) in meta.views"
      :key="index"
      :meta="itemMeta(index)"
      :is="item.name"
    />
  </td>
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object,
    data: Object
  },
  mounted() {
    if (this.data) this.$dispath("dataChange", { data: this.data });
  },
  watch: {
    data() {
      this.$dispath("dataChange", { data: this.data });
    }
  },
  methods: {
    itemMeta(index: number) {
      let meta = JSON.parse(JSON.stringify(this.meta.cellTemplate));
      meta.id += `_${index}`;
      for (const i of meta.hooks) {
        i.name += `_${index}`;
      }
      return meta;
    }
  }
});
</script>