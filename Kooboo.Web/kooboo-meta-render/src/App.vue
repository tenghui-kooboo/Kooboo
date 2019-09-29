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
import Vue from "vue";
export default Vue.extend({
  data() {
    return {
      meta: Object
    };
  }
});
</script>