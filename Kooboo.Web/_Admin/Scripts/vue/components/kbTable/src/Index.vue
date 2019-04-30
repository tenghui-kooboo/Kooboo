<template>
  <div>
    <inner-menu v-if="meta"
      :meta="meta.menu" :data="typeof data === 'object' ? data : null" :list="list"
      :selected="selected"></inner-menu>
    <inner-table v-if="meta"
      :meta="meta" :list="list"></inner-table>
  </div>
</template>

<script>
import InnerMenu from "./Menu.vue";
import InnerTable from "./Table.vue";

export default {
  name: "KoobooTable",

  props: {
    meta: Object,
    data: Object,
    listName: String
  },

  computed: {
    selected () {
      return this.list.filter(o => o.__selected)
    },

    list () {
      if (!this.data) {
        return []
      } else {
        return this.listName ? this.data[this.listName] : this.data
      }
    }
  },

  components: {
    InnerMenu,
    InnerTable
  }
};
</script>
