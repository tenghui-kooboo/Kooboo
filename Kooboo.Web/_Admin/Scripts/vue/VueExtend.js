Kooboo.Vue={
    data:{},
    execute:function(el){
        var vueData={};
        var models=this.data[el];
        if(models){
            for(var i=models.length-1;i>=0;i--){
                $.extend(true,vueData,models[i]);
            }
            var app=new Vue(vueData);
        }
        
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
