<template>
  <div class="form-group" v-show="visible">
    <label class="control-label col-md-3">{{ meta.label }}</label>
    <div class="col-md-9">
      <a class="btn btn-default btn-file"
        ><input
          type="file"
          accept="application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.ms-word.document.macroEnabled.12,application/msexcel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-powerpoint,application/vnd.openxmlformats-officedocument.presentationml.presentation,application/x-rar-compressed,application/octet-stream,application/x-zip-compressed,application/zip,application/octet-stream,text/html"
          :placeholder="meta.placeholder"
          :value="value"
          @input="valueChanged($event.target.value)"
        /><span>选择文件</span></a
      ><span>支持类型: HTML, PDF, Word, Excel, PPT, ZIP</span>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object,
    value: Object
  },
  data() {
    return {
      visible: true
    };
  },
  mounted() {
    this.visible = this.meta.visible;
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