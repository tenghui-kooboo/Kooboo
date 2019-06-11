window.fieldValidateMixin = Object.assign(window.vuelidate.validationMixin, {
  validations() {
    return {
      fieldValue: {
        rules: window.validators.rules(this.rules)
      }
    };
  }
});

window.formItemMixin = {
  data(){
    return {
      needFormat:true,
      fieldValue:this.data
    }
  },
  // computed:{
  //   fieldValue:{
  //     get(){
  //       return this.data
  //     }
  //   }
  // },
  created(){
    if(this.needFormat){
      var value=this.formatFieldValue();
      if(value){
        this.fieldValue =value;
      }
    }
    
  },
  inject: ["kbitem"],
  methods: {
    getValue(){
      return {
        invalid: this.$v.fieldValue.$invalid,
        value: this.fieldValue,
        name:this.name
      }
    },
    formatFieldValue() {
      if (this.options && this.options.data && this.options.text) {
        var data = this.ctx[this.options.data];
        if (data instanceof Array && data.length > 0) {
          data = data[0]
        }
        if (data instanceof Object) {
          var value = this.$parameterBinder.formatText(this.options.text, data);
          if (value) {
            return value
          }
        }
      } 
      return null;
    },
  },
  mounted() {
    this.kbitem.control = this;
  },

}