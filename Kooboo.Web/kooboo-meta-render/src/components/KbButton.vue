<template>
  <button class="btn navbar-btn green" @click="onClick">aa</button>
</template>

<script lang="ts">
import Vue from "./KbComponent.vue";
import { context } from "@/common/global";
export default Vue.extend({
  name: "kb-button",
  props: {
    meta: Object
  },
  data() {
    return {
      msg: "btn"
    };
  },
  methods: {
    onClick(e: any) {
      context.$emit(`click_${this.meta.id}`, {
        self: this.$el,
        e: e
      });
    }
  }
});
</script>

<style>
</style>