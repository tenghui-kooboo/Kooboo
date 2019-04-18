if(!window.Kooboo){
    window.Kooboo={};
}
Kooboo.Vue={
    data:{},
    vueHooks:["created"],//kvmodel only need support created
    getMergeHooks:function(models){
        var mergeHooks={};
        Kooboo.Vue.vueHooks.forEach(function(hook){
            var hookFuncs=[];
            for(var i=models.length-1;i>=0;i--){
                var model=models[i];
                if(model[hook]){
                    hookFuncs.push(model[hook]);
                }
            }
            if(models.length>0 &&hookFuncs.length>0){
                //mergeHooks[hook]=hookFuncs;
                mergeHooks[hook]=function(){
                    var vm=this;
                    hookFuncs.forEach(function(hookFunc){
                        hookFunc.call(vm);
                    });
                }
            }
        })
        return mergeHooks;
    },
    getVueData:function(data){
        var vueData={};
        var models=data;
        if(models){
            var mergeHooks =Kooboo.Vue.getMergeHooks(models);
            for(var i=models.length-1;i>=0;i--){
                $.extend(true,vueData,models[i]);
            }
            if(mergeHooks&&Object.keys(mergeHooks).length>0){
                $.extend(true,vueData,mergeHooks);
            }
            
            if(Vue.resetValid){
                Vue.resetValid(vueData)
            }
            
        }
        return vueData;
    }
}

Vue.kExtend=function(model){
   if(model.el){
    var models=Kooboo.Vue.data[model.el];
    if(!models){
        models=[];
    }
    models.push(model);
    Kooboo.Vue.data[model.el]=models;
   }
}

Vue.kExecute=function(){
    var data=Kooboo.Vue.data;
    if(data&&Object.keys(data).length>0){
        var keys=Object.keys(data);
        for(var i=0;i<keys.length;i++){
            var value=data[keys[i]];
            var vueData= Kooboo.Vue.getVueData(value);
            new Vue(vueData);
        }
    }
    
}

Vue.prototype.$parameterBinder=function(){
    var self=this;
    var ParameterBinder={
        bind:function(url){
            var model=self.$data;
            if(!url) return "";
            var keyValue=this.getUrlKeyValue(url);
            url=this.getUrl(url,keyValue,model);
    
            return url;
        },
        getUrl:function(url,keyValue,model){
            var keys=Object.keys(keyValue);
            url=url.split("?")[0];
            if(keys.length>0){
                url+="?";
            }
            for(var i=0;i<keys.length;i++){
                if(i>0){
                    url+="&"
                }
                var key=keys[i];
                var value=this.getQueryStringValue(model,keyValue[key]);
                url+=key+"="+value;
            }
            return url;
        },
        getUrlKeyValue:function(url){
            parts=url.split("?");
            var keyValue={};//default add siteid
            if(parts.length>1){
                var queryStringParts=parts[1].split("&");
                for(var i=0;i<queryStringParts.length;i++){
                    var part=queryStringParts[i];
                    var itemParts=part.split("=");
                    if(itemParts.length==2){
                        var key=itemParts[0];
                        var value=itemParts[1];
                        keyValue[key]=value;
                    }
                }
            }
            return keyValue;
        },
        isValuePlaceHolder:function(value){
            return value.indexOf("{")>-1 && value.indexOf("}")>-1;
        },
        getQueryStringValue:function(model,value){
            if(!this.isValuePlaceHolder(value)){
                return value;
            }
            var key=value.replace("{","").replace("}","").trim();
            var value=this.getValuebyModel(model,key);
            if(value){
                return value
            }
            value=this.getQueryString(key);
            if(value){
                return value
            }
                
            return "";
        },
        getValuebyModel:function(model,findKey){
            function getValue(data,field){
                var value="";
                if(!data[field]){
                    var keys=Object.keys(data);
                    for(var i=0;i<keys.length;i++){
                        var key=keys[i];
                        if(data[key] instanceof Object){
                            value=getValue(data[key],field);
                            if(value){
                                break;
                            }
                        }
                       
                    }
                }
                else{
                    value=data[field];
                }
               
                return value;
            }
            return getValue(model,findKey);
        },
        //code from kooboobase
        getQueryString:function(name, source) {
            if (!name) {
                return null;
            }
            source = source || window.location.search.substr(1);
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = source.match(reg);
            if (r != null) {
                return r[2];
            }
            return null;
        }
    
    }
    return ParameterBinder;
};
