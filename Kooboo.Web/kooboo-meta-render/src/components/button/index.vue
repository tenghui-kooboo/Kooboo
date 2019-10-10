<template >
  <a
    v-if="visible"
    :class="['btn navbar-btn btn-default', color]"
    @click="$dispath('click')"
  >
    <i v-if="icon" :class="icon" :style="iconStyle" />{{ text }}</a
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
    if (me.meta.icon != undefined) me.icon = me.meta.icon;
    if (me.meta.color != undefined) me.color = me.meta.color;
    if (me.meta.text != undefined) me.text = me.meta.text;
  },
  data() {
    return {
      visible: true,
      icon: "",
      color: "",
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
