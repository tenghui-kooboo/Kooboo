Vue.use(window.vuelidate.default);
var helpers = validators.helpers;

Vue.resetValid = function (vueData) {
    if (vueData.validations &&
        Object.keys(vueData.validations).length > 0) {
        vueData.validations = window.validators.Extend.extendValidations(vueData.validations);
    }
    return vueData;
}
validators.Extend = {
    validateRule: function (rules, value,vm) {
        if (!rules || rules.length == 0) {
            return {
                isValid: true,
                errors: []
            }
        };
        var isError = false;
        var errors = [];
        rules.forEach(function (rule) {
            var isValid = validators.Extend.isValid(rule, value,vm);
            if (!isValid) {
                isError = true;
                errors.push(rule.message);
            }
        })

        return {
            isValid: !isError,
            errors: errors
        }
    },
    isValid: function (rule, value,vm) {
        var isValid = true;

        var validator = validators[rule.type];
        if (validator) {
            var params = validators.Extend.getValidatorParams(rule);
            if (params.length > 0) {
                isValid = validator.apply(this, params)(value,vm);
            } else {
                isValid = validator(value);
            }
        }
        else {
            console.log("valid" + rule.type + "doesn't exist");
            return false;
        }
        return isValid;
    },
    getValidatorParams: function (rule) {
        if(rule.type=="sameAs"){
            var field=rule["field"];
            function equalTo(vm){
                return vm.$parameterBinder().getValuebyModel(vm.$data,field);
            }
            return [equalTo];
        }

        var validatorParamsKeys = {
            regex: ["regex"],
            minLength: ["minLength"],
            maxLength: ["maxLength"],
            between: ["from", "to"],
            unique:["api"],
            //sameAs: ["field"]
        };
        var keys = [];
        if (validatorParamsKeys[rule.type]) {
            keys = validatorParamsKeys[rule.type];
        }
        var params = [];
        keys.forEach(function (para) {
            params.push(rule[para]);
        })
        return params;
    },
    extendValidations: function (validations) {
        function getValidation(validation){
            var keys=Object.keys(validation);
            keys.forEach(function(key){
                var value=validation[key];
                if(value instanceof Array){
                    var rules=value;
                    validation[key]={
                        rules:validators.rules(rules)
                    }
                }else if(value instanceof Object){
                    validation[key]=getValidation(value);
                } 
            });
            return validation;
        }
        return getValidation(validations);
    }
}

validators.rules = function (rules) {
    var param = {
        type: "rules",
        rules: rules,
        errors:[]
    };
    return helpers.withParams(param, function (value) {
        var vm=this;
        //console.log(param.rules);
        //console.log(1);
        var result = validators.Extend.validateRule(rules, value,vm);
        //param.errors=param.errors.concat(result.errors);
        //todo check,param.errors=param.errors.concat(result.errors); 
        //can't refresh the errors
        param.errors.splice(0,param.errors.length);
        for(var i=0;i<result.errors.length;i++){
            param.errors.push(result.errors[i]);
        }

        return result.isValid;
    });
}
validators.regex = function (regex) {
    return function (value) {
        return validators.helpers.regex("regex", new RegExp(regex))(value);
    }
}

validators.unique = function (apiurl) {
    return function (value,vm) {
        if(!value) return true;

        var url=vm.$parameterBinder().bind(apiurl);
        var result=false;
        api.get(url,true,true).then(function(res){
            result=res.success;
        });
        return result;
    }
}
