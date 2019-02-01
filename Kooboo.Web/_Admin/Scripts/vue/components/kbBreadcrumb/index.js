(function() {
    Kooboo.vue.component.kbBreadcrumb = Vue.component('kb-breadcrumb', {
        props: {
            items: Array
        },
        data: function() {
            return {}
        },
        computed: {
            _items: function() {
                return this.items.map(function(item) {
                    var obj = null;
                    switch (item.name) {
                        case 'SITES':
                            obj = {
                                name: Kooboo.text.component.breadCrumb.sites,
                                url: Kooboo.Route.Site.ListPage
                            }
                            break;
                        case 'DASHBOARD':
                            obj = {
                                name: Kooboo.text.component.breadCrumb.dashboard,
                                url: Kooboo.Route.Get(Kooboo.Route.Site.DetailPage)
                            }
                            break;
                        default:
                            obj = item;
                            break;
                    }
                    return obj;
                })
            }
        },
        template: Kooboo.getTemplate('/_Admin/Scripts/vue/components/kbBreadcrumb/index.html')
    })
})()