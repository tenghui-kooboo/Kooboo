(function() {
  Kooboo.vue.component.kbFormItemHidden = Vue.component("kb-form-item-hidden", {
    props: {
      data: Object,
      name: String,
      rules: Array,
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
    mixins: [window.vuelidate.validationMixin],
    created() {
      this.fieldValue = this.data[this.name];
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/hidden.html"
    )
  });
})();
