if(!window.Kooboo){
    window.Kooboo={};
}
Kooboo.Vue={
    data:{},
    getVueData:function(data){
        var vueData={};
        var models=data;
        if(models){
            for(var i=models.length-1;i>=0;i--){
                $.extend(true,vueData,models[i]);
            }
            if(Vue.resetValid){
                Vue.resetValid(vueData)
            }
            
        }
        return vueData;
    },
    execute:function(el){
        var vueData= Kooboo.Vue.getVueData(this.data[el]);
        var app=new Vue(vueData);
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
