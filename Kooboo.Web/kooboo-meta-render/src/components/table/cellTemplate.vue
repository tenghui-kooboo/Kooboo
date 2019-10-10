<template>
  <component :meta="meta.view" :is="meta.view.name" />
</template>
<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    data: Object,
    meta: Object
  },
  mounted() {
    this.$dispath("dataChange", { data: this.data });
  },
  watch: {
    data() {
      this.$dispath("dataChange", { data: this.data });
    }
  }
});
</script>