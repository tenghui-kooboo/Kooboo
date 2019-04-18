export default {
  computed: {
    class () {
      if (!this.meta.class)
        return null

      if (!(this.meta.class instanceof Array)) {
        return this.meta.class
      }

      var result = ''

      for (var i = 0; i < this.meta.class.length; i++) {
        const seg = this.meta.class[i]
        if (typeof seg === 'object') {
          result += ' ' + seg[this.value]
        } else {
          result += ' ' + seg
        }
      }

      return result
    },

    value () {
      return this.row[this.name]
    },

    text () {
      if (!this.meta.text) {
        if (typeof this.value === "date") {
          return Date.toDefaultLangString(this.value)
        } else {
          return this.value
        }
      }

      if (typeof this.meta.text === 'object') {
        return this.formatText(this.meta.text[this.value])
      } else {
        return this.formatText(this.meta.text, this.value)
      }
    },

    visible () {
      if (this.meta.visible) {
        return this.meta.visible === this.value
      } else {
        return true
      }
    }
  },

  methods: {
    formatText (template, value) {
      var result = template.replace('{0}', value)
      result = result.replace(/\{([^\}]+)\}/g, (m, p) => eval(`Kooboo.text.${p}`))
      return result
    }
  }
}