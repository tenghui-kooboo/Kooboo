(function() {
  Kooboo.vue.component.kbFormItemTextarea = Vue.component(
    "kb-form-item-textarea",
    {
      props: {
        data: String,
        name: String,
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
        this.fieldValue = this.data;
        // this.fieldValue = this.data[this.name];
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textarea.html"
      )
    }
  );
})();
