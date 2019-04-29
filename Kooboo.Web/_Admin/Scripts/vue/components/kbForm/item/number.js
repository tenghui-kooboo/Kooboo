(function() {
  Kooboo.vue.component.kbFormItemNumber = Vue.component("kb-form-item-number", {
    props: {
      value: String,
      placeholder: String
    },
    data() {
      return {
        fieldValue: ""
      };
    },
    watch: {
      fieldValue(value) {
        this.$emit("fieldValue", {
          invalid: this.$v.fieldValue.$invalid,
          value: value
        });
      }
    },
    mixins: [window.fieldValidateMixin],
    created() {
      this.fieldValue = this.value;
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/number.html"
    )
  });
})();
