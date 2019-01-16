(function() {
    Kooboo.loadJS([
        '/_Admin/Scripts/vue/components/kbTable/row/index.js'
    ]);

    window.kbTable = Vue.component('kb-table', {
        props: {
            docs: Array,
            columns: Array,
            actions: Array,
            selectable: {
                type: Boolean,
                default: function() {
                    return true
                }
            }
        },
        data: function() {
            return {
                selectedDocs: []
            }
        },
        methods: {
            onToggleSelectDoc: function(id) {
                var idx = this.selectedDocs.indexOf(id);
                if (idx == -1) {
                    this.selectedDocs.push(id);
                } else {
                    this.selectedDocs.splice(idx, 1);
                }
            }
        },
        computed: {
            allSelected: {
                get: function() {
                    if (this.docs && this.docs.length) {
                        return this.selectedDocs.length == this.docs.length;
                    } else {
                        return false;
                    }
                },
                set: function(val) {
                    var self = this;
                    if (this.docs && this.docs.length) {
                        if (val) {
                            this.docs.forEach(function(doc) {
                                if (self.selectedDocs.indexOf(doc.id) == -1) {
                                    self.selectedDocs.push(doc.id);
                                }
                            })
                        } else {
                            self.selectedDocs = [];
                        }
                    } else {
                        self.selectedDocs = [];
                    }
                }
            }
        },
        components: {
            kbTableRow
        },
        template: Kooboo.getTemplate('/_Admin/Scripts/vue/components/kbTable/index.html')
    })
})()