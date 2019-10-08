<template>
  <div>
    <component
      :is="item.name"
      v-for="(item, index) in meta.views"
      :key="index"
      :meta="item"
    />
  </div>
</template>

<script lang="ts">
import Vue from "@/kbVue";

export default Vue.extend({
  props: {
    meta: Object
  }
});
</script>