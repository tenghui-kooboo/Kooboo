<template >
  <a
    v-if="visible"
    :class="['btn', 'navbar-btn', 'btn-default', ...classList]"
    @click="$dispath('click')"
  >
    <i v-if="icon" :class="icon" :style="iconStyle" />{{ meta.text }}</a
  >
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      visible: true,
      icon: "",
      classList: []
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
