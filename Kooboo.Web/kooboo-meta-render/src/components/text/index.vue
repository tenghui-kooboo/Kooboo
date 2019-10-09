<template>
  <span>{{meta.text}}</span>
</template>
<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  }
});
</script>