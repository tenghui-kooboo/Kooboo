
Kooboo.Table={
    getList:function(tableData,vm,api){
        if(!tableData.modelName){
            tableData.modelName=vm.modelName;
        }
        Kooboo.Table.execApi(api,{},function(data){
            Kooboo.Table.setDocs(data,tableData);
        })

        // var method=Kooboo.Table.getModel(tableData.modelName,"list");
        // method.then(function(res) {
        // //Kooboo.Layout.list().then(function(res) {
        //     if(res.success){
        //         var data=res.model;
        //         Kooboo.Table.setDocs(data,tableData);
        //         // var cols=tableData.cols;
        //         // for(var i=0;i<data.length;i++){
        //         //     var item=data[i];
        //         //     var doc=Kooboo.Table.getDoc(tableData,item);
                    
        //         //     tableData.docs.push(doc);
        //         // }
        //     }
        // });
    },
    execApi:function(api,paras,callback){
        var segs=api.split('.');
        if(segs.length!=3)
        console.log(api+" is error");

        var model=eval(segs[0]+"."+segs[1]);
        var method=segs[2];
        var method= model[method].call(model,paras);
        method.then(function(res){
            if(res.success){
                var data=res.model;
                callback(data);
            }
        });
    },
    setDocs:function(data,tableData){
        var docs=[];
        var cols=tableData.cols;
        tableData.docs=[];
        for(var i=0;i<data.length;i++){
            var item=data[i];
            var doc=Kooboo.Table.getDoc(tableData,item);
            
            tableData.docs.push(doc);
        }
        return docs;
    },
    copy:function(tableData,param,callback){
        var method=Kooboo.Table.getModel(tableData.modelName,"Copy",param);
        method.then(function(res) {
            if (res.success) {
                var doc=Kooboo.Table.getDoc(tableData,res.model);
                //put in beginning of an array.
                tableData.docs.unshift(doc);
            } 
            callback &&callback(res.success);
        })
    },
    getDoc:function(tableData,item){
        var doc={};
        var cols=tableData.cols;

        for(var j=0;j<cols.length;j++){
            var col=cols[j];
            doc[col.fieldName]=Kooboo.Table.getValue(item,col,tableData);
        }
        var rowActions=tableData.rowActions;
        if(rowActions.length>0){
            rowActions.forEach(function(action){
                doc[action.fieldName]=Kooboo.Table.getValue(item,action,tableData);
            });
        }
        if(!doc.id){
            doc.id=item.id;
        }
        return doc;
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

    getLinkUrl:function(data,col){
        var name=col.fieldName;
        var colData=col.data;
        
        if(colData.url){
            
            //this is a url from data
            //todo add url type
            debugger;
            if(data[colData.url]){
                return data[colData.url]
            }
            var urlData={};
            for(var i=0;i<colData.paras.length;i++){
                var para=colData.paras[i];
                urlData[para]=data[para];
            }
            var url= Kooboo.UrlHelper.Get(colData.url,urlData);
            if(!url){
                url="#"
            }
            return url;
        }
    },
    
    getValue:function(data,col,tableData){
        var urls=tableData.urls;
       
        switch(col.type){
            case Kooboo.Table.CellType.Array:
                var obj=data[col.fieldName];
                var arr=Kooboo.objToArr(obj);

                var tableArray=[];
                if(arr && arr.length>0){
                    var relation=col.data;
                    for(var i =0;i<arr.length;i++){
                        var item=arr[i];
                        tableArray.push({
                            text: item.value + ' ' + Kooboo.text.component.table[item.key],
                            class: 'label label-sm kb-table-label-refer',
                            style: 'background-color: ' + Kooboo.getLabelColor(item.key),
                            data: {
                                id: data.id,//todo rewrite
                                by: item.key,
                                type: tableData.modelName
                            },
                            onClick:function(rel){
                                tableData.modalConfig={
                                    data:rel.data,
                                    detailConfigs:col.data.config
                                };
                                tableData.isShowModal=true;
                            }
                        })
                    }
                }
                return tableArray;
            break;
            case Kooboo.Table.CellType.Badge:
                return {
                    text: data[col.fieldName],
                    class: data[col.fieldName] > 0 ? "blue" : "default",
                    onClick:function(doc){
                        tableData.modalConfig={
                            data:{id:doc.id},
                            detailConfigs:col.data.config
                        };
                        tableData.isShowModal=true;
                    }
                }
            break;
            case Kooboo.Table.CellType.Button:
                return {
                    class: 'btn-xs blue',
                    icon: "fa-clock-o",
                    title: col.displayName,
                    url: Kooboo.Table.getLinkUrl(data,col),
                    inNewWindow: true
                }
            break;
            case Kooboo.Table.CellType.IconText:
            break;
            case Kooboo.Table.CellType.Icon:
            break;
            case Kooboo.Table.CellType.Label:
            break;
            case Kooboo.Table.CellType.Link:
                return {
                    text:data[col.fieldName],
                    url:Kooboo.Table.getLinkUrl(data,col)
                };
            break;
            case Kooboo.Table.CellType.Summary:
            break;
            case Kooboo.Table.CellType.Text:
            default:
                var value=data[col.fieldName];
                if(col.dataType==Kooboo.Table.CellDataType.Date){
                    var date=new Date(value);
                    value=date.toDefaultLangString();
                }
                if(!value){
                    value="-"
                }
                return value;
            break;
        }
    },
    
    getRelationUrl:function(data){
        var id=data[objectId];
        var type=data["by"];
        switch (self.config.by) {
            case "layout":
                editUrl = Kooboo.Route.Get(Kooboo.Route.Layout.DetailPage, {
                    Id: id
                });
                break;
            case "view":
                editUrl = Kooboo.Route.Get(Kooboo.Route.View.DetailPage, {
                    Id: id
                });
                break;
            case "page":
                editUrl = Kooboo.Route.Get(Kooboo.Route.Page.EditRedirector, {
                    Id: id
                });
                break;
            case "textcontent":
                editUrl = Kooboo.Route.Get(Kooboo.Route.TextContent.DetailPage, {
                    Id: id
                });
                break;
            case "style":
            case "cssdeclaration":
                editUrl = Kooboo.Route.Get(Kooboo.Route.Style.DetailPage, {
                    Id: id
                });
                break;
            case "menu":
                editUrl = Kooboo.Route.Get(Kooboo.Route.Menu.DetailPage, {
                    Id: id
                });
                break;
            case "htmlblock":
                editUrl = Kooboo.Route.Get(Kooboo.Route.HtmlBlock.DetailPage, {
                    Id: id
                })
                break;
            case "form":
                editUrl = Kooboo.Route.Get(Kooboo.Route.Form.Redirector, {
                    Id: id
                })
                break;
            case "datamethodsetting":
                editUrl = Kooboo.Route.Get(Kooboo.Route.DataSource.DataMethodSetting, {
                    Id: id
                })
                break;
            default:
                editUrl = "";
                /*  window.info.show("Unhandle relation type: " + self.by(), false);
                console.warn("unhandle relation type:" + self.by());*/
                break;
        }
        return url;
    }
    // getValueByField:function(data,field){
    //     if(!field) return '';
    //     field=field[0].toLowerCase()+field.substring(1);
    //     return data[field];
    // }
}

Kooboo.Table.CellType={
        Array:"array",
        Badge:"badge",
        Button:"button",
        IconText:"iconText",
        Icon:"icon",
        Label:"label",
        Link:"link",
        Summary:"summary",
        Text:"text"
}
Kooboo.Table.CellDataType={
    Text:"Text",
    Date:"Date"
}