(function () {
  Kooboo.loadJS([
    "/_Admin/Scripts/vue/components/kbForm/index.js",
    "/_Admin/Scripts/vue/components/kbModal/index.js"
  ]);

  Kooboo.vue.component.kbPopup = Vue.component("kb-popup", {
    props: {
      visible: Boolean,
      meta: Object
    },
    data() {
      return {
        showModal: false,
        ctx: null
      };
    },
    methods: {
      show(ctx) {
        this.ctx = ctx;
        var modelName=this.getModelName(ctx.parameters);
        if(modelName){
          this.meta= api.getMeta(modelName);
        }

        this.showModal = true;
      },
      getModelName: function (parameters) {
        if (parameters["modelName"]) {
          return parameters["modelName"];
        }
        return "";
      },
    },
    
    watch: {
      visible(show) {
        if (show) {
          this.showModal = true;
        } else {
          this.showModal = false;
        }
      }
    },
    components: {
      "kb-modal": Kooboo.vue.component.kbModal,
      "kb-form": Kooboo.vue.component.kbForm
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbPopup/index.html"
    )
  });
})();