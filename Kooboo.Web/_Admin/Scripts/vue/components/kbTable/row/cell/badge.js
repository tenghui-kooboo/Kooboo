(function() {
    window.kbTableCellBadge = Vue.component('kb-table-cell-badge', {
        props: ['data'],
        render: function(createElement) {
            return createElement('td', [
                createElement('span', {
                    class: 'badge ' + this.data.class,
                    domProps: {
                        innerHTML: this.data.text
                    }
                })
            ]);
        }
    })
})()