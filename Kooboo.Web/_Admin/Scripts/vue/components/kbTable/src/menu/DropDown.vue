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
import actionMixin from '../actionMixin'

export default {
  name: 'MenuDropDown',

  props: {
    meta: Object,
    selected: Array,
    data: Object|Array,
    list: Array
  },

  data () {
    return {
      expanded: false
    }
  },

  mixins:[actionMixin],

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
       switch (this.actionType) {
        case 'post':
          api.post(this.bindUrl(this.action.url, option), { ids: this.selected.map(o => o.id) })
          break;
        case 'event':
          this.$root[this.action.url](this.selected)
          break;
        case 'popup':
          this.$root.$refs.popup.show({
            parameters: this.$parameterBinder.getKeyValue(this.action.url),
            context: this.list,
            selected: this.selected,
            meta: this.actionMeta
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
