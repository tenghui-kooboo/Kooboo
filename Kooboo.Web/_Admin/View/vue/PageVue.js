var pageListModel=[];
     Kooboo.Page.getAll().then(function(res) {
        if (res.success) {
            pageListModel=res.model;
            // BASE_URL = res.model.baseUrl;
            // vm.tableData(generatePageTableData(res.model.pages));
            // vm.layouts(getLayoutList(res.model.layouts));
            // vm.pages(res.model.pages);
            // ko.applyBindings(vm, document.getElementById("main"));
        }
    });

    var vm=new Vue({
        el:"#app",
        data:{
            createNewPageUrl:Kooboo.Route.Get(Kooboo.Route.Page.EditPage),
            createNewConntentPageUrl:Kooboo.Route.Get(Kooboo.Route.Page.EditRichText),
            pageListModel:pageListModel,
            isShowCopyBtn:true,
            isShowCopyModel:false,
            copyModalConfig:{
                //title:Kooboo.text.site.page.copyPage + ': ' + copyPage().name,
                title:Kooboo.text.site.page.copyPage,
                closeBtnText:"Cancel",
                showCloseBtn:true,
                btns:[{
                    text:"Start",
                    class:"green",
                    onClick:function(){

                    }
                }]
            }
        },
        computed:{
            layouts:function(){
                var layouts=pageListModel.layouts;
                var _list = [];

                _.forEach(layouts, function(layout) {
                    _list.push({
                        name: layout.name,
                        href: Kooboo.Route.Get(Kooboo.Route.Page.EditLayout, {
                            layoutId: layout.id
                        })
                    })
                });

                if (!_list.length) {
                    _list.push({
                        name: Kooboo.text.site.page.createNewLayout,
                        href: Kooboo.Route.Get(Kooboo.Route.Layout.Create)
                    })
                }

                return _list;
            }
        },
        methods:{
            showCopyModal:function(){
                this.isShowCopyModel=true
            },
            hideCopyModal:function(){
                this.isShowCopyModel=false
            }
        },

    })