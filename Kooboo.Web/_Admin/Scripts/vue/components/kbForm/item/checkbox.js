(function() {
  Kooboo.vue.component.kbFormItemCheckbox = Vue.component(
    "kb-form-item-checkbox",
    {
      props: {
        data: Object,
        name: String,
        extra: Array,
        optionConfig: Object,
        placeholder: String
      },
      data() {
        return { options: [] };
      },
      // mixins: [fieldItemMixins],
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
      mounted() {
        var self = this;
        if (this.optionConfig.default) {
          this.options.push(this.optionConfig.default);
        }
        this.options = this.options.concat(
          this.extra.map(function(data) {
            return {
              displayName: data[self.optionConfig.displayName],
              value: data[self.optionConfig.value]
            };
          })
        );
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/checkbox.html"
      )
    }
  );
})();
