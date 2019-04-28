(function() {
  Kooboo.vue.component.kbFormItemTextarea = Vue.component(
    "kb-form-item-textarea",
    {
      props: {
        data: Object,
        name: String,
        placeholder: String
      },
      data() {
        return {
          fieldValue: ""
        };
      },
      mixins: [window.fieldValidateMixin],
      created() {
        this.fieldValue = this.data[this.name];
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/textarea.html"
      )
    }
  );
})();
