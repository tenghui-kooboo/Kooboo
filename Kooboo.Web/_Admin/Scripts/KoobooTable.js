Kooboo.Table={
    getList:function(tableData){
        var modelName=tableData.modelName;
        var method=Kooboo.Table.getModel(modelName,"list");
        method.then(function(res) {
        //Kooboo.Layout.list().then(function(res) {
            if(res.success){
                var data=res.model;
                var cols=tableData.cols;

                var urls=tableData.urls;
                for(var i=0;i<data.length;i++){
                    var doc={};
                    var item=data[i];
                    for(var j=0;j<cols.length;j++){
                        var col=cols[j];
                        doc[col.fieldName]=Kooboo.Table.getValue(item,col,modelName,tableData);
                    }
                    var rowActions=tableData.rowActions;
                    if(rowActions.length>0){
                        rowActions.forEach(function(action){
                            doc[action.fieldName]=Kooboo.Table.getValue(item,action,modelName,tableData);
                        });
                    }
                    if(!doc.id){
                        doc.id=item.id;
                    }
                    tableData.docs.push(doc);
                }
            }
        });
    },
    getModel:function(modelName,methodName,param){
        var model=eval("Kooboo."+modelName);
        if(param){
           return model[methodName].call(model,param);
        }
        return model[methodName].call(model);
    },
    //refactor when move KoobooBase.js
    deletes:function(tableData,selectedIds,callback){
        if (confirm(Kooboo.text.confirm.deleteItems)) {
            var modelName=tableData.modelName;
            var method=Kooboo.Table.getModel(modelName,"Deletes",{
                ids: JSON.stringify(selectedIds)
            });
            
            method.then(function(res) {
                if(res.success){
                    callback && callback();

                    for(var i=0;i<selectedIds.length;i++){
                        var id=selectedIds[i];
                        var index=_.findIndex(tableData.docs,function(doc){
                            return doc.id==id;
                        });
                        tableData.docs.splice(index,1);

                    }
                    //Kooboo.Table.getList(tableData);
                }
               
                // layouts.getList(function() {
                //     if (res.success) {
                //         info.done(Kooboo.text.info.delete.success);
                //     }
                // })
            })
        }
    },
    //todo refactor
    //will remove kooboo.base.js
    // getListMethod:function(api){
    //     var segs=api.split('.');
    //     if(segs.length!=3)
    //         console.log(api+" is error");
    //     var model=eval(segs[0]+"."+segs[1]);
    //     var method=segs[2];
    //     return model[method].call(model);

    // },
    getLinkUrl:function(data,col,urls){
        var name=col.fieldName;
        var link=_.find(urls,function(url){
            return url.fieldName.toLowerCase()==name.toLowerCase();
        });
        
        if(link!=null){
            var urlData={};
            for(var i=0;i<link.paras.length;i++){
                var para=link.paras[i];
                urlData[para]=data[para];
            }
            var url=eval(link.url);
            return  Kooboo.Route.Get(url,urlData);
        }
        return "";
    },
    getValue:function(data,col,modelType,tableData){
        var urls=tableData.urls;
        var relations=tableData.relations;
        switch(col.type.toLowerCase()){
            case 'array':
                var obj=data[col.fieldName];
                var arr=Kooboo.objToArr(obj);

                var tableArray=[];
                if(arr && arr.length>0){
                    var relation=_.find(relations,function(rel){
                                    return rel.fieldName.toLowerCase()==col.fieldName.toLowerCase();
                                });
                    for(var i =0;i<arr.length;i++){
                        var item=arr[i];
                        tableArray.push({
                            text: item.value + ' ' + Kooboo.text.component.table[item.key],
                            class: 'label label-sm kb-table-label-refer',
                            style: 'background-color: ' + Kooboo.getLabelColor(item.key),
                            data: {
                                id: data.id,//todo rewrite
                                by: item.key,
                                type: modelType
                            },
                            onClick:function(rel){
                                relation.config=rel.data;
                                relation.isShow=true;
                            }
                        })
                    }
                }
                return tableArray;
            break;
            case 'badge':
            
            break;
            case 'button':
                return {
                    class: 'btn-xs blue',
                    icon: "fa-clock-o",
                    title: col.displayName,
                    url: Kooboo.Table.getLinkUrl(data,col,urls),
                    inNewWindow: true
                }
            break;
            case 'icontext':
            break;
            case 'icon':
            break;
            case 'label':
            break;
            case 'link':
                return {
                    text:data[col.fieldName],
                    url:Kooboo.Table.getLinkUrl(data,col,urls)
                };
            break;
            case 'summary':
            break;

            case 'text':
            default:
                var value=data[col.fieldName];
                if(col.dataType=="Date"){
                    var date=new Date(value);
                    value=date.toDefaultLangString();
                }
                return value;
            break;
        }
    }
}