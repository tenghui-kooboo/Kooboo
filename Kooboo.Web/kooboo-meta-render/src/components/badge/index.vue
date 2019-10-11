<template>
  <a class="badge">{{ text }}</a>
</template>
<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      text: ""
    };
  },
  mounted() {
    if (this.meta.text != undefined) this.text = this.meta.text;
  }
});
</script>