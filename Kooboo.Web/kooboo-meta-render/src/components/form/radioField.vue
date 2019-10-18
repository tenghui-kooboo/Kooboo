<template>
  <div class="form-group" v-show="visible">
    <label class="control-label col-md-3">{{ meta.label }}</label>
    <div class="col-md-9">
      <label
        class="radio-inline"
        v-for="(item, index) in meta.items"
        :key="index"
        ><input
          type="radio"
          :value="item.value"
          @input="value = $event.target.value"
          :name="meta.field"
          :checked="value == item.value"
        />{{ item.label }}</label
      >
    </div>
  </div>
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
      value: ""
    };
  },
  mounted() {
    this.visible = this.meta.visible;
    this.value = this.meta.defaultValue;
  },
  watch: {
    value(value) {
      this.$dispath("valueChanged", {
        value: value
      });
      this.$emit("input", value);
    }
  }
});
</script>