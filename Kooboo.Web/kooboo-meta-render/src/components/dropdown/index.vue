<template>
  <div class="btn-group navbar-btn">
    <button class="btn green" data-toggle="dropdown" aria-expanded="false">
      <span>{{ meta.text }}</span> <i class="fa fa-angle-down"></i>
    </button>
    <ul class="dropdown-menu">
      <template v-if="meta.itemTemplate">
        <dropdown-item
          v-for="(item, index) in dataList"
          :meta="itemMeta(index)"
          :key="index"
          :data="item"
        />
      </template>
      <template v-else>
        <dropdown-item
          v-for="(item, index) in meta.items"
          :meta="item"
          :key="index"
        />
      </template>
    </ul>
  </div>
</template>

<script lang="ts">
import Vue from "@/common/kbVue";
import DropdownItem from "./item.vue";
export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      dataList: []
    };
  },
  methods: {
    itemMeta(index: number) {
      let meta = JSON.parse(JSON.stringify(this.meta.itemTemplate));
      meta.id += `_${index}`;
      for (const i of meta.hooks) {
        i.name += `_${index}`;
      }
      return meta;
    }
  },
  components: {
    DropdownItem
  }
});
</script>