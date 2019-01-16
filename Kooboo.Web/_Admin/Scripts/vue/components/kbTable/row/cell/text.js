(function() {
    window.kbTableCellText = Vue.component('kb-table-cell-text', {
        props: ['data'],
        render: function(h) {
            return h('td', {
                domProps: {
                    innerHTML: this.data
                }
            })
        }
    })
})()