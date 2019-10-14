<template>
  <form class="form-horizontal">
    <form-field
      v-for="(item, index) in meta.fields"
      :key="index"
      :meta="item"
    />
  </form>
</template>

<script lang="ts">
import Vue from "vue";
import FormField from "./field.vue";
export default Vue.extend({
  props: {
    meta: Object
  },
  components: {
    FormField
  }
});
</script>