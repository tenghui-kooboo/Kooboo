(function () {
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
      extra: Array
    },
    data() {
      return {
        meta: {}
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
        var submitUrl = this.meta.submitApi;
        debugger;
      }
    },
    mounted() {
      var self = this;
      if (this.metaName) {
        this.meta = {
          loadApi: "",
          submitApi: "/page/copy",
          layout: "horizontal",
          items: [
            {
              type: "textBox",
              label: "name",
              name: "name",
              placeholder: "",
              class: "col-md-9",
              tooltip: "",
              options: {
                data: "selected", //context:selected/list
                text: "{name}_copy"
              },
              rules: [
                {
                  type: "required",
                  message: "required"
                },
                {
                  type: "between",
                  from: 1,
                  to: 64,
                  message: "minLength 1,maxLength 64"
                },
                {
                  type: "unique",
                  api: "/page/isUnique?name={name}",
                  message: "taken"
                }
              ]
            },
            {
              type: "hidden",
              label: "id",
              name: "id",
              options: {
                data: "selected", //context:selected/list
                text: "id"
              }
            },
            {
              type: "hidden",
              label: "url",
              name: "url",
              options: {
                data: "selected",
                text: "/{name}_copy"
              },
              rules: [
                {
                  type: "required",
                  message: "required"
                },
                {
                  type: "regex",
                  regex:
                    "^[^s|~|`|!|@|#|$|%|^|&|*|(|)|+|=|||[|]|;|:|\"|'|,|<|>|?]*$",
                  message: "invalid"
                }
              ]
            }
          ]
        };

        if (this.meta.loadApi) {
          api
            .get(this.$parameterBinder().bind(this.meta.loadApi))
            .then(function(res) {
              debugger;
              if (res.success) {
                self.data = res.model;
              }
            });
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
        debugger;
        return api.post(this.meta.submitApi);
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