<template>
  <div class="form-group">
    <label for class="control-label" :class="{ 'col-md-3': horizontal }"></label>
    <div :class="{ 'col-md-9': horizontal }">
      <template v-if="_ct == 'textbox'">
        <input type="text" class="form-control" v-model="value">
      </template>
      <template v-else-if="_ct == 'textarea'">
        <textarea type="text" class="form-control" v-model="value"></textarea>
      </template>
      <template v-else-if="_ct == 'richeditor'">richeditor</template>
      <template v-else-if="_ct == 'checkbox'">checkbox</template>
      <template v-else-if="_ct == 'radiobox'">radiobox</template>
      <template v-else-if="_ct == 'selection'">selection</template>
      <template v-else-if="_ct == 'number'">number</template>
      <template v-else-if="_ct == 'datetime'">datetime</template>
      <template v-else-if="_ct == 'switch'">switch</template>
      <template v-else-if="_ct == 'upload'">upload</template>
    </div>
  </div>
</template>
<script>
export default {
  name: "FromItem",
  props: {
    controlType: {
      type: String,
      default: "TextBox"
    },
    label: String,
    value: String,
    tooltip: String,
    placeholder: String,
    horizontal: Boolean
  },
  computed: {
    _ct() {
      return this.controlType.toLowerCase();
    }
  }
};
</script>

