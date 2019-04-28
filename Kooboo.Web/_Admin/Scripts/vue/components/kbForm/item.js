(function() {
  Kooboo.loadJS([
    "/_Admin/Scripts/vue/components/kbForm/item/textbox.js",
    "/_Admin/Scripts/vue/components/kbForm/item/textarea.js",
    "/_Admin/Scripts/vue/components/kbForm/item/checkbox.js",
    "/_Admin/Scripts/vue/components/kbForm/item/radiobox.js",
    "/_Admin/Scripts/vue/components/kbForm/item/hidden.js",
    "/_Admin/Scripts/vue/components/kbForm/item/selection.js",
    "/_Admin/Scripts/vue/components/kbForm/item/number.js",
    "/_Admin/Scripts/vue/components/kbForm/item/datetime.js"
  ]);

  Kooboo.vue.component.kbFormItem = Vue.component("kb-form-item", {
    props: {
      idx: Number,
      ctx: Object,
      label: String,
      tooltip: String,
      options: Object,
      externalClass: String,
      placeholder: String,
      controlType: String,
      horizontal: Boolean,
      data: Object,
      rules: Array,
      name: String
    },
    data() {
      return {
        rendered: false
      };
    },
    computed: {
      _ct() {
        return this.controlType.toLowerCase();
      },
      isFieldInvalid() {
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
    mounted() {
      this.rendered = true;
    },
    components: {
      "kb-form-item-hidden": Kooboo.vue.component.kbFormItemHidden,
      "kb-form-item-number": Kooboo.vue.component.kbFormItemNumber,
      "kb-form-item-textbox": Kooboo.vue.component.kbFormItemTextbox,
      "kb-form-item-datetime": Kooboo.vue.component.kbFormItemDatetime,
      "kb-form-item-textarea": Kooboo.vue.component.kbFormItemTextarea,
      "kb-form-item-checkbox": Kooboo.vue.component.kbFormItemCheckbox,
      "kb-form-item-radiobox": Kooboo.vue.component.kbFormItemRadiobox,
      "kb-form-item-selection": Kooboo.vue.component.kbFormItemSelection
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item.html"
    )
  });
})();
