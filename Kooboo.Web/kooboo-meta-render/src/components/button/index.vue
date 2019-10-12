<template >
  <a v-show="visible" class="btn navbar-btn" @click="$dispath('click')">
    <kb-icon v-if="meta.icon" :meta="meta.icon" />
    <span v-if="text && meta.icon">&nbsp;</span>
    {{ text }}
  </a>
</template>

<script lang="ts">
import Vue from "@/kbVue";

export default Vue.extend({
  props: {
    meta: Object
  },
  async mounted() {
    if (this.meta.visible != undefined) this.visible = this.meta.visible;
    if (this.meta.text != undefined) this.text = this.meta.text;
  },
  data() {
    return {
      visible: true,
      text: ""
    };
  }
});
</script>
