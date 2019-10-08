<template>
  <li>
    <a @click="$dispath('click')">{{ text }}</a>
  </li>
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object,
    data: Object
  },
  data() {
    return {
      text: ""
    };
  },
  mounted() {
    this.text = this.meta.text;
    if (this.data) this.$dispath("dataChange", { data: this.data });
  },
  watch: {
    data() {
      this.$dispath("dataChange", { data: this.data });
    }
  }
});
</script>