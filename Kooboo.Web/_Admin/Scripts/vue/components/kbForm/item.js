(function() {
  Kooboo.loadJS([
    "/_Admin/Scripts/vue/components/kbForm/item/textbox.js",
    "/_Admin/Scripts/vue/components/kbForm/item/textarea.js"
  ]);

  Kooboo.vue.component.kbFormItem = Vue.component("kb-form-item", {
    props: {
      label: String,
      value: String,
      tooltip: String,
      externalClass: String,
      placeholder: String,
      controlType: String
    },
    data() {},
    computed: {
      _ct() {
        return this.controlType.toLowerCase();
      }
    },
    components: {
      "kb-form-item-textbox": Kooboo.vue.component.kbFormItemTextbox,
      "kb-form-item-textarea": Kooboo.vue.component.kbFormItemTextarea
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/item.html"
    )
  });
})();
