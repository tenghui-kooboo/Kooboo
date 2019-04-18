(function() {
  Kooboo.vue.component.kbFormItemRadiobox = Vue.component(
    "kb-form-item-radiobox",
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
        "/_Admin/Scripts/vue/components/kbForm/item/radiobox.html"
      )
    }
  );
})();
