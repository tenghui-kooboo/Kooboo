(function() {
  Kooboo.vue.component.kbFormItemDatetime = Vue.component(
    "kb-form-item-datetime",
    {
      props: {
        value: String,
        placeholder: String
      },
      data() {
        return { fieldValue: "" };
      },
      created() {
        this.fieldValue = this.value;
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/datetime.html"
      )
    }
  );
})();
