<template>
  <div class="form-group" v-show="visible">
    <label class="control-label col-md-3">{{ meta.label }}</label>
    <div class="col-md-9">
      <input
        type="text"
        class="form-control"
        :placeholder="meta.placeholder"
        :value="value"
        @input="valueChanged($event.target.value)"
      />
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
  methods: {
    valueChanged(value: string) {
      this.$dispath("valueChanged", {
        value: value
      });
      this.$emit("input", value);
    }
  }
});
</script>