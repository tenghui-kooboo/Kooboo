Kooboo.Vue={
    data:[],
    extend:function(vueData){
        this.data.push(vueData);
    },
    execute:function(){
        var vueData={};
        
        for(var i=this.data.length-1;i>=0;i--){
            $.extend(true,vueData,this.data[i]);
        }
        var app=new Vue(vueData);
    }
}