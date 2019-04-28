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
    mixins: [window.vuelidate.validationMixin],
    created() {
      this.fieldValue = this.data[this.name];
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/hidden.html"
    )
  });
})();
