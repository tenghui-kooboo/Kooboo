(function() {
  Kooboo.vue.component.kbFormItemSelection = Vue.component(
    "kb-form-item-selection",
    {
      props: {
        value: String,
        options: Array,
        placeholder: String
      },
      data() {
        return { fieldValue: "" };
      },
      mounted() {
        this.fieldValue = this.value;
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/selection.html"
      )
    }
  );
})();
