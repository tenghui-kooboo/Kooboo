(function() {
    Kooboo.loadJS([
        '/_Admin/Scripts/vue/components/kbTable/row/cell/text.js',
        '/_Admin/Scripts/vue/components/kbTable/row/cell/badge.js',
        '/_Admin/Scripts/vue/components/kbTable/row/cell/label.js',
        '/_Admin/Scripts/vue/components/kbTable/row/cell/link.js'
    ]);

    window.kbTableRow = Vue.component('kb-table-row', {
        props: ['doc', 'columns', 'actions', 'selectable', 'selectedDocs'],
        methods: {
            getCellData: function(data) {
                return this.doc[data.fieldName]
            },
            onToggleSelectDoc: function() {
                this.$emit('toggleSelectDoc', this.doc.id);
            }
        },
        components: {
            kbTableCellText,
            kbTableCellBadge,
            kbTableCellLabel,
            kbTableCellLink,
        },
        template: Kooboo.getTemplate('/_Admin/Scripts/vue/components/kbTable/row/index.html')
    })
})()