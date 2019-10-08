<template>
  <li>
    <a @click="$dispath('click')">{{ data ? data[meta.text] : meta.text }}</a>
  </li>
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object,
    data: Object
  }
});
</script>