(function() {
    Kooboo.loadJS([
        '/_Admin/Scripts/vue/components/kbBreadcrumb/item/index.js'
    ])

    Kooboo.vue.component.kbBreadcrumb = Vue.component('kb-breadcrumb', {
        data: function() {
            return {}
        },
        template: Kooboo.getTemplate('/_Admin/Scripts/vue/components/kbBreadcrumb/index.html')
    })
})()