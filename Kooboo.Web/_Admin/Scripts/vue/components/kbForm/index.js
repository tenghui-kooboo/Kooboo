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
        formData: {},
        fieldsValue: []
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
    created() {
      var self = this;
      if (this.metaName) {
        this.meta=api.getMeta(this.metaName);
        if (this.meta.loadApi) {
          api
            .get(this.$parameterBinder.bind(this.meta.loadApi))
            .then(function(res) {
              if (res.success) {
                Vue.set(self, "formData", res.model);
              }
            });
        } else {
          this.formData = this.data;
        }
      }
    },
    watch: {
      formData(data) {
        if (this.meta && this.meta.items && this.meta.items.length > 0) {
          this.meta.items.forEach(function(item) {
            Vue.set(item, "data", data[item.name]);
          });
        }
      }
    },
    methods: {
      valueChange(obj) {
        let idx = this.fieldsValue.findIndex(function(field) {
          return field.name == obj.name;
        });
        if (idx > -1) {
          this.fieldsValue.splice(idx, 1, obj);
        } else {
          this.fieldsValue.push(obj);
        }
      },
      getFieldsValue() {
        let res = {};
        this.fieldsValue.forEach(function(field) {
          res[field.name] = field.value;
        });
        return res;
      },
      validate(cb) {
        let hasError = this.fieldsValue.filter(function(field) {
          return field.invalid;
        });

        cb && cb(hasError.length > 0);
      },
      submit: function() {
        var self = this;
        return new Promise(function(resolve, reject) {
          api
            .post(
              self.$parameterBinder.bind(self.meta.submitApi),
              self.getFieldsValue()
            )
            .then(function(res) {
              resolve(res);
            })
            .catch(function(ex) {
              reject(ex);
            });
        });
      },
      reset() {
        Vue.set(this, "formData", {});
        //this.formData = {};
        this.fieldsValue = [];
        this.meta = {};
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
