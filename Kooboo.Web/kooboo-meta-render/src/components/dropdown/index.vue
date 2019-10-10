<template>
  <div class="btn-group navbar-btn">
    <button
      :class="['btn btn-default', color]"
      data-toggle="dropdown"
      aria-expanded="false"
    >
      <span>{{ text }}</span> <i class="fa fa-angle-down"></i>
    </button>
    <ul class="dropdown-menu">
      <template v-if="meta.itemTemplate">
        <dropdown-item
          v-for="(item, index) in items"
          :meta="changeMetaId(meta.itemTemplate)"
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
import Vue from "@/kbVue";
import DropdownItem from "./item.vue";
import { changeMetaId } from "@/common/utils";
export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      items: [],
      color: "",
      text: ""
    };
  },
  methods: {
    changeMetaId: changeMetaId
  },
  mounted() {
    if (this.meta.color != undefined) this.color = this.meta.color;
    if (this.meta.text != undefined) this.text = this.meta.text;
  },
  components: {
    DropdownItem
  }
});
</script>