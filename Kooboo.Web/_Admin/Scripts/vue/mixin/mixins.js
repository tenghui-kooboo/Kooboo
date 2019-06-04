window.formItem = {
  created(){
    this.fieldValue = this.fieldData();
  },
  methods: {
    fieldData() {
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
      } else if (this.data) {
        return this.data
      }
    }
  }

}