export default {
  data() {
    return {
      needFormat: true,
      fieldValue: this.data
    }
  },
  
  created() {
    if (!this.fieldValue) {
      var value = this.formatFieldValue();
      if (value) {
        this.fieldValue = value;
      }
    }

  },

  inject: ["kbitem"],

  methods: {
    getValue() {
      return {
        invalid: this.$v.fieldValue.$invalid,
        value: this.fieldValue,
        name: this.name
      }
    },
    formatFieldValue() {
      if (this.kbitem.defaultValue && this.kbitem.defaultValue instanceof Object) {

        var defaultValue = this.kbitem.defaultValue
        var data = this.ctx[defaultValue.dataField]
        if (data instanceof Array && data.length > 0) {
          data = data[0]
        }
        if (data instanceof Object) {
          var value = this.$parameterBinder.formatText(defaultValue.value, data);
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