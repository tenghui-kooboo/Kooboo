Vue.use(window.vuelidate.default);
var helpers=validators.helpers;

Vue.resetValid=function(vueData){
    if(vueData.validations && 
        Object.keys(vueData.validations).length>0){
        vueData.validations=window.validators.resetValidations(vueData.validations);
    }
}
validators.Extend={
    validRule:function(rules,value){
        if(!rules||rules.length==0) {
            return {
                isValid:true,
                errors:[]
            }
        };
        var isError=false;
        rules.forEach(function(rule){
            var isValid=validators.Extend.isValid(rule,value);
            if(!isValid){
                isError=true;
            }
        })
    
        return {
            isValid:!isError,
            errors:[]
        }
    },
    isValid:function(rule,value){
        var isValid=true;
        
        var validator=validators[rule.type];;
        if(validator){
            var params=validators.Extend.getValidatorParams(rule);
            if(params.length>0){
                isValid=validator.apply(this,params)(value);
            }else{
                isValid=validator(value);
            }
            
        }else{
            console.log("valid"+rule.type+"doesn't exist");
            return false;
        }
        return isValid;
    },
    // getValidator:function(ruleType){
    //     var validator=validators[ruleType];
    //     if(!validator){
    //         validator= validators.helpers[ruleType]
    //     }
    //     return validator;
    // },
    getValidatorParams:function(rule){
        var validatorParamsKeys={
            regex:["regex"],
            minLength:["minLength"],
            maxLength:["maxLength"],
            between:["from","to"],
            sameAs:["field"]
        };
        var keys=[];
        if(validatorParamsKeys[rule.type]){
            keys=validatorParamsKeys[rule.type];
        }
        var params=[];
        keys.forEach(function(para){
            params.push(rule[para]);
        })
        return params;
    },
    resetValidations:function(validations){
        var keys=Object.keys(validations);
        keys.forEach(function(key){
            var rules=validations[key];
            validations[key]={multiRule:validators.multiRule(rules)}    
        });
        return validations;
    }
}

//vuevalidate extend
validators.multiRule=function(rules){
    var param={type:"multiRule",rules:rules};
    return helpers.withParams(param,function(value){
        var result= validators.Extend.validRule(rules,value);
        param.errors=result.errors;

        return result.isValid;
    });
}
validators.regex=function(regex){
    return function(value){
        return validators.helpers.regex("regex",new RegExp(regex))(value);
    }
}
validators.unique=function(urlvalue){
    return function(value){
        //url=//parameterbinder.bind(url);
        //todo need wrap ajaxpost
        //api post
        //return 
    }
}
