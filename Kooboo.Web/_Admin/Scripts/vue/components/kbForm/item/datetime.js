(function() {
  Kooboo.vue.component.kbFormItemDatetime = Vue.component(
    "kb-form-item-datetime",
    {
      props: {
        data: String,
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
      mixins: [window.fieldValidateMixin],
      created() {
        this.fieldValue = this.data;
        // this.fieldValue = this.data[this.name];
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/datetime.html"
      )
    }
  );
})();
