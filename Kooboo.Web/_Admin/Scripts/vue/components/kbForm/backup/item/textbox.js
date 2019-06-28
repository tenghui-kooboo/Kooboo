(function() {
  Kooboo.vue.component.kbFormItemTextbox = Vue.component(
    "kb-form-item-textbox",
    {

      props: {
        data: String,
        name: String,
        rules: Array,
        options: Object|Array,
        ctx: Object,
        placeholder: String
      },

      mixins: [window.fieldValidateMixin,window.formItemMixin],
      
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textbox.html"
      )
    }
  );
})();
