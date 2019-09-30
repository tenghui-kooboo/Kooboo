<template>
  <a class="btn navbar-btn green" @click="$dispath('click')">{{ meta.text }}</a>
</template>

<script lang="ts">
import Vue from "@/common/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  }
});
</script>

<style>
</style>