<template >
  <a v-show="visible" class="btn navbar-btn" @click="$dispath('click')">
    <kb-icon v-if="meta.icon" :meta="meta.icon" :style="iconStyle" />{{
      text
    }}</a
  >
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  },
  mounted() {
    let me: any = this;
    if (me.meta.visible != undefined) me.visible = me.meta.visible;
    if (me.meta.text != undefined) me.text = me.meta.text;
  },
  data() {
    return {
      visible: true,
      text: ""
    };
  },
  computed: {
    iconStyle() {
      return {
        "margin-right": (this.meta as any).text ? "5px" : ""
      };
    }
  }
});
</script>
