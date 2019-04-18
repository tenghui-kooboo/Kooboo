// const files = require.context('./', false, /\.vue$/)
// const types = files.keys().map(o => o.replace(/(\.\/|\.vue)/g, '')).filter(o => !o.startsWith('_'))
// const components = {}

// for (var i = 0; i < types.length; i++) {
//   components[`Menu${types[i]}`] = require(`./${types[i]}.vue`)
// }
import MenuButton from './Button'
import MenuDropdown from './DropDown'
import MenuIcon from './Icon'

export default {
  MenuButton,
  MenuDropdown,
  MenuIcon
}
