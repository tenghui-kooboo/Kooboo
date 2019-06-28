(function() {
  Kooboo.vue.component.kbFormItemNumber = Vue.component("kb-form-item-number", {
    
    props: {
      data: String,
      options: Object|Array,
      name: String,
      ctx: Object,
      placeholder: String
    },
 
    mixins: [window.fieldValidateMixin,window.formItemMixin],
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/number.html"
    )
  });
})();
