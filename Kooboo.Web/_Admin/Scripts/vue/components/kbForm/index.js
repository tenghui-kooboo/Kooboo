(function() {
  Kooboo.loadJS(["/_Admin/Scripts/vue/components/kbForm/item.js"]);

  Kooboo.vue.component.kbForm = Vue.component("kb-form", {
    props: {
      metaName: String,
      data: {
        type: Object,
        default: function() {
          return {};
        }
      },
      ctx: Object
    },
    data() {
      return {
        meta: {},
        formData: null
      };
    },
    computed: {
      layout() {
        if (this.meta) {
          switch (this.meta.layout) {
            case "horizontal":
              return "form-horizontal";
            case "inline":
              return "form-inline";
            default:
              return "";
          }
        } else {
          return "";
        }
      }
    },
    methods: {
      submit: function() {
        debugger;
        return api.post(this.$parameterBinder().bind(this.meta.submitApi));
      }
    },
    mounted() {
      var self = this;
      if (this.metaName) {
        this.meta = {
          loadApi: "/Page/DefaultRoute",
          submitApi: "/page/defaultRouteUpdate",
          items: [
            {
              type: "selection",
              label: "Home page",
              name: "startPage",
              placeholder: "",
              class: "col-md-9",
              tooltip: "",
              options: {
                data: "context", //context:selected/list
                text: "{name}",
                value: "{id}",
                default: {
                  text: "System default",
                  value: "00000000-0000-0000-0000-000000000000"
                }
              }
            },
            {
              type: "selection",
              label: "404 page",
              name: "notFound",
              placeholder: "",
              class: "col-md-9",
              tooltip: "",
              options: {
                data: "context", //context:selected/list
                text: "{name}",
                value: "{id}",
                default: {
                  text: "System default",
                  value: "00000000-0000-0000-0000-000000000000"
                }
              }
            },
            {
              type: "selection",
              label: "Error page",
              name: "error",
              placeholder: "",
              class: "col-md-9",
              tooltip: "",
              options: {
                data: "context", //context:selected/list
                text: "{name}",
                value: "{id}",
                default: {
                  text: "System default",
                  value: "00000000-0000-0000-0000-000000000000"
                }
              }
            }
          ]
        };

        if (this.meta.loadApi) {
          api
            .get(this.$parameterBinder().bind(this.meta.loadApi))
            .then(function(res) {
              debugger;
              if (res.success) {
                self.formData = res.model;
              }
            });
        } else {
          this.formData = this.data;
        }
        // api.getMeta(this.metaName).then(function(res) {
        //   debugger;
        // });
      }
    },
    methods: {
      validate() {
        debugger;
      },
      submit() {
        return api.post(this.$parameterBinder().bind(this.meta.submitApi));
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
