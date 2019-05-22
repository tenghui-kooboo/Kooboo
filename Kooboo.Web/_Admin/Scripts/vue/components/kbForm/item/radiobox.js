(function() {
  Kooboo.vue.component.kbFormItemRadiobox = Vue.component(
    "kb-form-item-radiobox",
    {
      props: {
        data: String,
        name: String,
        optionConfig: Object,
        placeholder: String
      },
      data() {
        return { options: [], fieldValue: "" };
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
      mounted() {
        var self = this;
        if (this.optionConfig.default) {
          this.options.push(this.optionConfig.default);
        }
        // this.options = this.options.concat(
        //   this.extra.map(function(data) {
        //     return {
        //       displayName: data[self.optionConfig.displayName],
        //       value: data[self.optionConfig.value]
        //     };
        //   })
        // );
        this.fieldValue = this.data;
        // this.fieldValue = this.data[this.name];
      },
      template: Kooboo.getTemplate(
        "/_Admin/Scripts/vue/components/kbForm/item/radiobox.html"
      )
    }
  );
})();
