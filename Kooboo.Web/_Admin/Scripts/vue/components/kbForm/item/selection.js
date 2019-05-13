(function () {
  Kooboo.vue.component.kbFormItemSelection = Vue.component(
    "kb-form-item-selection", {
      props: {
        data: String,
        name: String,
        options: Object,
        placeholder: String,
        ctx: Object
      },
      data() {
        return {
          finalOptions: [],
          fieldValue: ""
        };
      },
      watch: {
        data(data) {
          this.fieldValue = this.data;
          // this.fieldValue = this.data[this.name];
        },
        fieldValue(value) {
          this.$emit("fieldValue", {
            invalid: this.$v.fieldValue.$invalid,
            value: value
          });
        }
      },
      mixins: [window.fieldValidateMixin],
      mounted() {
        var self = this;
        if (this.options.default) {
          this.finalOptions.push({
            displayName: this.options.default.text,
            value: this.options.default.value
          });
        }

        if (this.options instanceof Array) {
          this.finalOptions = this.finalOptions.concat(this.options);
        } else if (this.options instanceof Object) {
          var self = this;
          this.finalOptions = this.finalOptions.concat(
            this.ctx[this.options.data].map(function (item) {
              return {
                displayName: self
                  .$parameterBinder
                  .formatText(self.options.text, item),
                value: self
                  .$parameterBinder
                  .formatText(self.options.value, item)
              };
            })
          );
        }
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/selection.html"
      )
    }
  );
})();