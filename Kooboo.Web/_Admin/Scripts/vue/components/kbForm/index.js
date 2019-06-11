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
        fields:[],
      };
    },
    provide(){
      return {
        kbform:this
      }
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
              self.setFormData(res.model)
            }
          });
      } else {
        self.setFormData(this.data)
      }
    },
    methods: {
      setFormData(data){
        if (this.meta_d && this.meta_d.items && this.meta_d.items.length > 0) {
          this.meta_d.items.forEach(function(item) {
            if(data){
              item.data=data[item.name]
            }
          });
        }
      },
      getFieldsValue() {
        let res = {};
        this.fields.forEach(function(field) {
          var fieldValue=field.getValue();
          if(fieldValue){
            res[fieldValue.name] = fieldValue.value;
          }
          
        });
        return res;
      },
      validate(cb) {
        let hasError = this.fields.filter(function(field) {
          var fieldValue=field.getValue();
          return fieldValue && fieldValue.invalid;
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
        this.formData = {};
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
