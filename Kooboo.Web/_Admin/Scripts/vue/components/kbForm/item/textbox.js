(function() {
  Kooboo.vue.component.kbFormItemTextbox = Vue.component(
    "kb-form-item-textbox",
    {
      props: {
        data: String,
        name: String,
        rules: Array,
        options: Object,
        ctx: Object,
        placeholder: String
      },
      data() {
        return { fieldValue: "" };
      },
      watch: {
        fieldValue(value) {
          this.$emit("fieldValue", {
            invalid: this.$v.fieldValue.$invalid,
            value: value
          });
        }
      },
      mixins: [window.fieldValidateMixin,window.formItem],
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textbox.html"
      )
    }
  );
})();
