<template >
  <a v-if="visible" class="btn navbar-btn green" @click="$dispath('click')">{{
    meta.text
  }}</a>
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      visible: true
    };
  }
});
</script>

<style>
</style>