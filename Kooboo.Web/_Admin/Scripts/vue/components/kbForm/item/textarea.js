(function() {
  Kooboo.vue.component.kbFormItemTextarea = Vue.component(
    "kb-form-item-textarea",
    {
      props: {
        data: String,
        name: String,
        options: Object,
        ctx: Object,
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
      mixins: [window.fieldValidateMixin,window.formItem],
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textarea.html"
      )
    }
  );
})();
