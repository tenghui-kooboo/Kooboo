(function() {
  Kooboo.loadJS(["/_Admin/Scripts/vue/components/kbForm/item.js"]);

  Kooboo.vue.component.kbForm = Vue.component("kb-form", {
    props: {
      meta: Object
    },
    computed: {
      layout() {
        switch (this.meta.layout) {
          case "horizontal":
            return "form-horizontal";
          case "inline":
            return "form-inline";
          default:
            return "";
        }
      }
    },
    components: {
      "kb-form-item": Kooboo.vue.component.kbFormItem
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbForm/index.html"
    )
  });
})();
