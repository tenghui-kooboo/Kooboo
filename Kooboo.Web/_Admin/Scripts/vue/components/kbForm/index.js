(function() {
  Kooboo.loadJS(["/_Admin/Scripts/vue/components/kbForm/item.js"]);

  Kooboo.vue.component.kbForm = Vue.component("kb-form", {
    props: {
      metaName: String,
      meta:Object,
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
        meta_d: this.meta,
        formData: {},
        fieldsValue: []
      };
    },
    computed: {
      layout() {
        if (this.meta_d) {
          switch (this.meta_d.layout) {
            case "horizontal":
            case 0:
              return "form-horizontal";
            case "inline":
            case 1:
              return "form-inline";
            default:
              return "";
          }
        } else {
          return "";
        }
      },
      isHorizontal(){
        return this.meta_d.layout=="horizontal"||this.meta_d.layout===0
      },
    },
    
    created() {
      var self = this;
      if (this.metaName) {
        this.meta_d=api.getMeta(this.metaName);//support metaname
      }
      if (this.meta_d && this.meta_d.loadApi) {
        api
          .get(this.$parameterBinder.bind(this.meta_d.loadApi))
          .then(function(res) {
            if (res.success) {
              Vue.set(self, "formData", res.model);
            }
          });
      } else {
        this.formData = this.data;
      }
    },
    watch: {
      formData(data) {
        if (this.meta_d && this.meta_d.items && this.meta_d.items.length > 0) {
          this.meta_d.items.forEach(function(item) {
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
              self.$parameterBinder.bind(self.meta_d.submitApi),
              self.getFieldsValue()
            )
            .then(function(res) {
              resolve(res);
            })
            ;
        });
      },
      reset() {
        Vue.set(this, "formData", {});
        //this.formData = {};
        this.fieldsValue = [];
        this.meta_d = {};
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
