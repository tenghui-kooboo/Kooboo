(function() {
  Kooboo.vue.component.kbFormItemHidden = Vue.component("kb-form-item-hidden", {
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
    mixins: [window.vuelidate.validationMixin,window.formItem],
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/hidden.html"
    )
  });
})();
