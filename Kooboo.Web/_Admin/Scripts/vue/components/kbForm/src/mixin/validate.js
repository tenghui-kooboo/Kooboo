export default Object.assign(window.vuelidate.validationMixin, {
  validations() {
    return {
      fieldValue: {
        rules: window.validators.rules(this.rules)
      }
    };
  }
});