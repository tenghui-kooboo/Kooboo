<template>
  <kb-button :meta="meta" />
</template>
<script lang="ts">
import Vue from "vue";
import meta from "../_mock/button-meta.json";
export default Vue.extend({
  data() {
    return {
      meta: meta
    };
  }
});
</script>