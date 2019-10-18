<template>
  <form class="form-horizontal">
    <component
      v-for="(item, index) in meta.fields"
      :key="index"
      :meta="item"
      :is="item.name"
    />
  </form>
</template>

<script lang="ts">
import Vue from "vue";
import TextField from "./textField.vue";
import RadioField from "./radioField.vue";
import FileField from "./fileField.vue";
export default Vue.extend({
  props: {
    meta: Object,
    data: Object
  },
  components: {
    TextField,
    RadioField,
    FileField
  }
});
</script>