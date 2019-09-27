<template>
  <button class="btn navbar-btn green" @click="onClick">aa</button>
</template>

<script lang="ts">
import Vue from "vue";
import { context } from "@/common/global";
export default Vue.extend({
  name: "kb-button",
  props: {
    meta: Object
  },
  created() {
    if (this.meta.hooks && this.meta.hooks instanceof Array) {
      for (const i of this.meta.hooks) {
        context.$on(i.name, () => eval(i.action));
      }
    }
  },
  methods: {
    onClick() {
      context.$emit(`${this.meta.name}_click`);
    }
  }
});
</script>

<style>
</style>