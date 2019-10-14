<template>
  <div class="form-group">
    <label class="control-label col-md-3" >{{
      meta.label
    }}</label>
    <div class="col-md-9">
      <input
        type="email"
        class="form-control"
        placeholder="Email"
      />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    meta: Object
  }
});
</script>