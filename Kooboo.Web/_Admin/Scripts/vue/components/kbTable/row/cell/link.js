(function() {
    window.kbTableCellLink = Vue.component('kb-table-cell-link', {
        props: ['data'],
        render: function(createElement) {
            var self = this;
            if (this.data) {
                return createElement('td', [
                    createElement('a', {
                        attrs: {
                            title: this.data.title || '',
                            href: this.data.url
                        },
                        domProps: {
                            innerHTML: this.data.text
                        },
                        on: {
                            click: function(e) {
                                if (self.data.type == 'event') {
                                    e.preventDefault();
                                    Kooboo.EventBus.publish(self.data.url, self.data);
                                }
                            }
                        }
                    })
                ])
            } else {
                return createElement('td', [
                    createElement('a', {
                        domProps: {
                            innerHTML: ''
                        }
                    })
                ]);
            }
        }
    })
})()