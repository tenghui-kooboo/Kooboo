(function() {
  Kooboo.vue.component.kbFormItemSelection = Vue.component(
    "kb-form-item-selection",
    {
      props: {
        data: Object,
        name: String,
        extra: Array,
        optionConfig: Object,
        placeholder: String
      },
      data() {
        return { options: [], fieldValue: "" };
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
        this.fieldValue = this.data[this.name];
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/selection.html"
      )
    }
  );
})();
