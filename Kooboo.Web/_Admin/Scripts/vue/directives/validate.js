(function() {
    Vue.directive('validate', {
        bind: function(el, binding, vnode) {
            debugger
            // binding.value.isValid = true;
            // binding.value.errors = [];

            el.value = binding.value.value || '';

            el.addEventListener('blur', function() {
                binding.value.value = el.value;
                binding.def.update(el, binding);
            })
        },
        update: function(el, binding) {
            var passed = true;
            var errors = [];

            binding.value.validate.rules.forEach(function(rule) {
                switch (rule.type) {
                    case 'required':
                        if (!el.value) {
                            passed = false;
                            errors.push(rule.message || 'This field is required.')
                        }
                        break;
                }
            })

            binding.value.isValid = passed;
            binding.value.errors = errors;
        }
    })
})()