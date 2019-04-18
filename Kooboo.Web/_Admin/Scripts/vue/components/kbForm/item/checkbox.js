(function() {
  Kooboo.vue.component.kbFormItemCheckbox = Vue.component(
    "kb-form-item-checkbox",
    {
      props: {
        value: String,
        options: Array,
        placeholder: String
      },
      data() {
        return {
          finalValue: ""
        };
      },
      computed: {
        fieldValue: {
          get: function() {
            return JSON.parse(this.value || "[]");
          },
          set: function(value) {
            this.finalValue = JSON.stringify(value);
          }
        }
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/checkbox.html"
      )
    }
  );
})();
