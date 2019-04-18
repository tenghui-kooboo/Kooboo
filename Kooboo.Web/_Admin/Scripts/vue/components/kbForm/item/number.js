(function() {
  Kooboo.vue.component.kbFormItemNumber = Vue.component("kb-form-item-number", {
    props: {
      value: String,
      placeholder: String
    },
    data() {
      return {
        fieldValue: ""
      };
    },
    created() {
      this.fieldValue = this.value;
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item/number.html"
    )
  });
})();
