<template>
  <div
    class="navbar navbar-default"
    v-if="meta && meta.views && meta.views.length > 0"
  >
    <div class="container-fluid">
      <component
        v-for="(item, index) in meta.views"
        :key="index"
        :is="item.name"
        :meta="item"
      />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "./KbComponent.vue";
export default Vue.extend({
  props: {
    meta: Object
  }
});
</script>