<template>
  <form class="form" :class="layoutClass">
    <form-item
      v-for="(item, idx) in meta.items"
      :key="idx"
      :label="item.label"
      :value="item.value"
      :tooltip="item.tooltip"
      :controlType="item.type"
      :placeholder="item.placeholder"
      :horizontal="meta.layout=='horizontal'"
    ></form-item>
  </form>
</template>
<script>
import FormItem from "./item";

export default {
  name: "KbForm",
  props: {
    meta: {
      type: Object
    }
  },
  computed: {
    layoutClass() {
      switch (this.meta.layout) {
        case "horizontal":
          return "form-horizontal";
        case "inline":
          return "form-inline";
        default:
          return "";
      }
    }
  },
  components: { FormItem }
};
</script>
