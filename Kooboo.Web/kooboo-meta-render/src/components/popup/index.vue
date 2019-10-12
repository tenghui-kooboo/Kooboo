<template>
  <div
    class="modal fade"
    data-backdrop="static"
    data-keyboard="false"
    :id="meta.id"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button class="close" @click="visible = false">
            <i class="fa fa-close"></i>
          </button>
          <h4 class="modal-title">{{ meta.title }}</h4>
        </div>
        <div class="modal-body" ref="body">
          <component v-if="meta.body" :is="meta.body.name" :meta="meta.body" />
        </div>
        <div class="modal-footer">
          <kb-button
            v-for="(item, index) in meta.footer"
            :meta="item"
            :key="index"
          />
        </div>
      </div>
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
      visible: false
    };
  },
  mounted() {
    if (this.meta.visible != undefined) this.visible = this.meta.visible;
  },
  watch: {
    visible(visible: boolean) {
      this.onVisibleChange(visible);
    }
  },
  methods: {
    onVisibleChange(visible: boolean) {
      this.$dispath(visible ? "show" : "close");
      ($(`#${this.meta.id}`) as any).modal(visible ? "show" : "hide");
    }
  }
});
</script>