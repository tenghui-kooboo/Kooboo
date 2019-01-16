(function() {
    window.kbTableCellLabel = Vue.component('kb-table-cell-label', {
        props: ['data'],
        render: function(createElement) {
            return createElement('td', [
                createElement('span', {
                    class: 'label ' + (this.data.size ? 'label-' + this.data.size + ' ' : ' ') + this.data.class,
                    domProps: {
                        innerHTML: this.data.text
                    }
                })
            ]);
        }
    })
})()