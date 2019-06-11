(function() {
  Kooboo.vue.component.kbFormItemRadiobox = Vue.component(
    "kb-form-item-radiobox",
    {
      props: {
        data: String,
        name: String,
        options: Array|Object,
        placeholder: String
      },

      mixins: [window.fieldValidateMixin,window.formItemMixin],

      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/radiobox.html"
      )
    }
  );
})();
