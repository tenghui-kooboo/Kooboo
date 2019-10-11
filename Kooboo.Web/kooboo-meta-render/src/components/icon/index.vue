<template>
  <i v-if="visible" :class="iconName" />
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      iconName: "",
      visible: true
    };
  },
  mounted() {
    if (this.meta.iconName != undefined) this.iconName = this.meta.iconName;
    if (this.meta.visible != undefined) this.visible = this.meta.visible;
  }
});
</script>