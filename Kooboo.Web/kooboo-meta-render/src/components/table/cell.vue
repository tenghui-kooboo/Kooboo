<template>
  <td>
    <component
      v-for="(item, index) in meta.views"
      :key="index"
      :meta="item"
      :is="item.name"
    />
  </td>
</template>

<script lang="ts">
import Vue from "@/kbVue";
export default Vue.extend({
  props: {
    meta: Object
  }
});
</script>