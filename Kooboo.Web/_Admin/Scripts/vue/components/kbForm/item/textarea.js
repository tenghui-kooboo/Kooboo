(function() {
  Kooboo.vue.component.kbFormItemTextarea = Vue.component(
    "kb-form-item-textarea",
    {
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
        "/_Admin/Scripts/vue/components/kbForm/item/textarea.html"
      )
    }
  );
})();
