Vue.use(window.vuelidate.default);
var helpers=validators.helpers;

ruleValid=function(rules){
    var data={errors:[]};
    
    return helpers.withParams(data,function(value){
        
        if(rules.length>0){
            data.errors.splice(0,data.errors.length);
            
            var isError=false;

            rules.forEach(function(rule){
                var isValid=true;
                switch(rule.type){
                    case "regex":
                    case "email":
                        isValid=helpers.regex(rule.regex);
                        break;
                    case "minLength":
                        isValid= validators.minLength(rule.minLength)(value);  
                    break;
                    case "maxLength":
                        isValid= validators.maxLength(rule.maxLength)(value);  
                    break;
                    case "between":
                        isValid= validators.between(rule.from,rule.to)(value);  
                    break;
                    //required,numeric,ipAddress,integer
                    default:
                        var valid=validators[rule.type];
                        if(valid){
                            isValid=valid(value);
                        }else{
                            console.log("valid"+rule.type+"doesn't exist")
                        }
                        break;
                }
                if(!isValid){
                    isError=true;
                    if(rule.message){
                        data.errors.push(rule.message);
                    }
                    
                }
                
            });
            return !isError;
        }
        return true;
    })
}
