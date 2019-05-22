(function() {
  Kooboo.vue.component.kbFormItemCheckbox = Vue.component(
    "kb-form-item-checkbox",
    {
      props: {
        data: String,
        name: String,
        optionConfig: Object,
        placeholder: String
      },
      data() {
        return { options: [] };
      },
      watch: {
        fieldValue(value) {
          this.$emit("fieldValue", {
            invalid: this.$v.fieldValue.$invalid,
            value: value
          });
        }
      },
      mixins: [window.fieldValidateMixin],
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
