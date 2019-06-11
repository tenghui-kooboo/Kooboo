(function() {
  Kooboo.vue.component.kbFormItemCheckbox = Vue.component(
    "kb-form-item-checkbox",
    {
      props: {
        data: String,
        name: String,
        options: Array|Object,
        placeholder: String
      },

      mixins: [window.fieldValidateMixin,window.formItemMixin],

      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/checkbox.html"
      )
    }
  );
})();
