// const files = require.context('./', false, /\.vue$/)
// const types = files.keys().map(o => o.replace(/(\.\/|\.vue)/g, '')).filter(o => !o.startsWith('_'))
// const components = {}

// for (var i = 0; i < types.length; i++) {
//   components[`Cell${types[i]}`] = require(`./${types[i]}.vue`)
// }
import CellArray from './Array'
import CellBadge from './Badge'
import CellButton from './Button'
import CellIcon from './Icon'
import CellLabel from './Label'
import CellLink from './Link'
import CellText from './Text'
import CellDate from './Date'

export default {
  CellArray,
  CellBadge,
  CellButton,
  CellIcon,
  CellLabel,
  CellLink,
  CellText,
  CellDate
}
