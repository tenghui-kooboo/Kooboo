(function () {
  Kooboo.vue.component.kbFormItemSelection = Vue.component(
    "kb-form-item-selection", {
      
      props: {
        data: String,
        name: String,
        options: Object|Array,
        placeholder: String,
        ctx: Object
      },

      data() {
        return {
          fieldValue: this.data
        };
      },

      mixins: [window.fieldValidateMixin, window.formItemMixin],

      computed: {
        finalOptions() {
          var finalOptions=[];
          
          if (this.kbitem.placeholder && this.kbitem.defaultValue && this.kbitem.defaultValue.value) {
            finalOptions.push({
              displayName: this.kbitem.placeholder,
              value: this.kbitem.defaultValue.value
            });
          }

          if (this.options instanceof Array) {
            finalOptions = finalOptions.concat(this.options);
          } else if (this.options instanceof Object) {
            var self = this;
            finalOptions = finalOptions.concat(
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

          return finalOptions;
        }
      },
      
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/selection.html"
      )
    }
  );
})();