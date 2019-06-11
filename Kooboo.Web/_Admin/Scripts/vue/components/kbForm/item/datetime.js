(function() {
  Kooboo.vue.component.kbFormItemDatetime = Vue.component(
    "kb-form-item-datetime",
    {
      
      props: {
        data: String,
        name: String,
        placeholder: String
      },

      mixins: [window.fieldValidateMixin,window.formItemMixin],

      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/datetime.html"
      )
    }
  );
})();
