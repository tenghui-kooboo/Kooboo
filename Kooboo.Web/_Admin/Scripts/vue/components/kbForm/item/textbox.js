(function() {
  Kooboo.vue.component.kbFormItemTextbox = Vue.component(
    "kb-form-item-textbox",
    {
      props: {
        data: Object,
        name: String,
        placeholder: String
      },
      data() {
        return { fieldValue: "" };
      },
      created() {
        this.fieldValue = this.data[this.name];
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textbox.html"
      )
    }
  );
})();
