(function() {
  Kooboo.vue.component.kbFormItemTextarea = Vue.component(
    "kb-form-item-textarea",
    {
      
      props: {
        data: String,
        name: String,
        options: Object|Array,
        ctx: Object,
        placeholder: String
      },

      mixins: [window.fieldValidateMixin,window.formItemMixin],

      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textarea.html"
      )
    }
  );
})();
