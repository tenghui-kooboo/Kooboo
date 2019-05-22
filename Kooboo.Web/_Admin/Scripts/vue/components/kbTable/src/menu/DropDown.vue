<template>
  <div :class="[ 'btn-group navbar-btn', { open: expanded }]">
    <button :class="`btn ${meta.class}`" data-toggle="dropdown" @click.prevent="expanded = !expanded">
      <span>{{ meta.text }}</span>
      <i class="fa fa-angle-down"></i>
    </button>
    <ul class="dropdown-menu">
      <li v-for="(option, index) in options" :key="index">
        <inner-button :meta="meta" :selected="selected"
          :bind-url="url => bindUrl(url, option.value)"
          @click="onClick(option.value)">{{ option.text }}</inner-button>
      </li>
    </ul>
  </div>
</template>

<script>
import InnerButton from '../components/_Button'

export default {
  name: 'MenuDropDown',

  props: {
    meta: Object,
    selected: Array,
    data: Object,
    list: Array
  },

  data () {
    return {
      expanded: false
    }
  },

  computed: {
    options () {
      const options = this.meta.options
      if(options instanceof Array){
        return options.map(o => ({ text: o.text, value: o }))
      }else if(options instanceof Object){
        const data = this.data[options.data];
        console.log(data);
        var self=this;
        return data.map(function(o){
          var text=self.$parameterBinder.formatText(options.text,o);
          var url=self.$parameterBinder.bind(url,o);
          return {
            text:text,
            value:url
          };
        })
      }

      return [];
    }
  },

  methods: {
    bindUrl (url, option) {
      return this.$parameterBinder.bind(url)
    },

    onClick (option) {
       switch (this.meta.action) {
        case 'post':
          api.post(this.bindUrl(this.meta.url, option), { ids: this.selected.map(o => o.id) })
          break;
        case 'event':
          this.$root[this.meta.url](this.selected)
          break;
        default:
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(this.meta.url),
            context: this.list,
            selected: this.selected
          })
          break;
      }
    }
  },

  components: {
    InnerButton
  }
}
</script>
