<template>
  <td>
    <template v-if="meta.itemTemplate">
      <cell-template
        v-for="(item, index) in items"
        :key="index"
        :meta="changeMetaId(meta.itemTemplate)"
        :data="item"
      />
    </template>
    <template v-else>
      <component
        v-for="(item, index) in meta.views"
        :key="index"
        :meta="changeMetaId(item)"
        :is="item.name"
      />
    </template>
  </td>
</template>

<script lang="ts">
import Vue from "@/kbVue";
import CellTemplate from "./cellTemplate.vue";
import { changeMetaId } from "@/common/utils";

export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      items: []
    };
  },
  methods: {
    changeMetaId: changeMetaId
  },
  watch: {
    items() {
      this.$dispath("dataChange", { data: this.items });
    }
  },
  components: {
    CellTemplate
  }
});
</script>