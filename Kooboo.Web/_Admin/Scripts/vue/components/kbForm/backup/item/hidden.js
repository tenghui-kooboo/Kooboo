(function() {
  Kooboo.vue.component.kbFormItemHidden = Vue.component("kb-form-item-hidden", {
    
    props: {
      data: String,
      name: String,
      rules: Array,
      options: Object|Array,
      ctx: Object,
      placeholder: String
    },

    mixins: [window.vuelidate.validationMixin,window.formItemMixin],

    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/hidden.html"
    )
  });
})();
