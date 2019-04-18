import './index.js'
import test_data from './test_data'

new Vue({
  el: '#app',
  data: {
    meta: test_data.meta,
    data: test_data.data
  },
  template: '<kb-table :meta="meta" :data="data" list-name="pages"></kb-table>'
})