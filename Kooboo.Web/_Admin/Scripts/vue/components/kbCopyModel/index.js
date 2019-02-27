(function() {
    Kooboo.loadJS([
        '/_Admin/Scripts/vue/components/kbModal/index.js'
    ]);

    Kooboo.vue.component.kbCopyModel = Vue.component('kb-copy-modal', {
        props: {
            config: Object,
            show: Boolean
        },
        data: function() {
            return {
                showModal: false,
                modalConfig: {
                    //title: 'Copy layout',
                    title: 'Copy',
                    size: 'sm',
                    copyLayoutName: '',
                    btns: [{
                        text: Kooboo.text.common.start,
                        class: 'green',
                        onClick: function() {
                            var title="layout.name.text";
                            this.modalConfig.title += (': ' + title);
                            this.modalConfig.copyLayoutName = title + '_Copy'
                            // var layout = layouts.tableData.docs.find(function(doc) {
                            //     return doc.id == layouts.selectedDocs[0];
                            // })
                            // if (layout) {
                            //     Kooboo.Layout.Copy({
                            //         id: layout.id,
                            //         name: layouts.modalConfig.copyLayoutName
                            //     }).then(function(res) {
                            //         layouts.showCopyModal = false;
                            //         if (res.success) {
                            //             layouts.getList(function() {
                            //                 info.done(Kooboo.text.info.copy.success);
                            //             });
                            //         } else {
                            //             info.fail(Kooboo.text.info.copy.fail);
                            //         }
                            //     })
                            // }
                        }
                    }],
                    onShow: function() {
                        var layout = layouts.tableData.docs.find(function(doc) {
                            return doc.id == layouts.selectedDocs[0];
                        })

                        if (layout) {
                            layouts.modalConfig.title += (': ' + layout.name.text);
                            layouts.modalConfig.copyLayoutName = layout.name.text + '_Copy'
                        }
                    },
                    onClose: function() {
                        this.copyLayoutName = ''
                    },
                    showCloseBtn: true,
                    closeBtnText: Kooboo.text.common.cancel,
                    labelName: Kooboo.text.common.name,
                }
            }
        },
        methods: {
            onClose: function() {
                this.showModal = false;
            }
        },
        watch:{
            show:function(show){
                if(show){
                    this.showModal=true;
                }
            }
        },
        components: {
            'kb-modal': Kooboo.vue.component.kbModal
        },
        template: Kooboo.getTemplate('/_Admin/Scripts/vue/components/kbCopyModel/index.html')
    })
})()