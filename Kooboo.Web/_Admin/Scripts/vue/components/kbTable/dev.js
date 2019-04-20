import './index.js'
import test_data from './test_data'

Vue.component("kb-modal", {
  name: 'popup',

  methods: {
    show (obj) {
      console.log(obj)
    }
  },

  template: '<div>Popup</div>'
})

new Vue({
  el: '#app',
  data: {
    meta: test_data.meta,
    data: test_data.data
  },
  template: '<div><kb-table :meta="meta" :data="data" list-name="pages"></kb-table><kb-modal ref="popup" /></div>'
})