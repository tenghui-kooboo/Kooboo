<template>
  <div class="form-group" :class="{ hidden: _ct == 'hidden', 'has-error': isFieldInvalid }">
    <!-- <div class="form-group" :class="{ hidden: _ct == 'hidden'}"> -->
    <label class="control-label" :class="{ 'col-md-3': horizontal }">
      {{
      label
      }}
    </label>
    <div :class="{ 'col-md-9': horizontal }">
      <component
        :ctx="ctx"
        :name="name"
        :data="data"
        :rules="rules"
        :class="externalClass"
        :ref="`field_item_${idx}`"
        :is="`kb-${_ct}`"
        :placeholder="placeholder"
        :options="options"
      ></component>
      <template v-if="isFieldInvalid">
        <span class="help-block" v-for="(err, index) in fieldErrors" :key="index">{{ err }}</span>
      </template>
      <span class="help-block" v-if="tooltip">{{ tooltip }}</span>
    </div>
  </div>
</template>

<script>
import itemComponents from './item'

export default {
  props: {
    idx: Number,
    ctx: Object,
    label: String,
    tooltip: String,
    options: Object | Array,
    externalClass: String,
    placeholder: String,
    defaultValue: String | Object,
    controlType: String,
    horizontal: Boolean,
    data: String,
    rules: Array,
    name: String,
  },
  data() {
    return {
      rendered: false,
      control: null,
    };
  },
  provide() {
    return {
      kbitem: this
    }
  },
  inject: ['kbform'],
  computed: {
    _ct() {
      if (!this.controlType) {
      }
      return this.controlType.toLowerCase();
    },
    isFieldInvalid() {
      debugger
      if (this.rendered) {
        return this.$refs[`field_item_${this.idx}`].$v.fieldValue.$invalid;
      } else {
        return false;
      }
    },
    fieldErrors() {
      if (this.rendered) {
        return this.$refs[`field_item_${this.idx}`].$v.fieldValue.$params
          .rules.errors;
      } else {
        return false;
      }
    }
  },
  methods: {
    getValue() {
      if (this.control && this.control.getValue) {
        return this.control.getValue();
      }
      return null;
    }
  },
  mounted() {
    this.kbform.fields.push(this)
    this.rendered = true;
  },
  components:{
    ...itemComponents
  }
}
</script>
