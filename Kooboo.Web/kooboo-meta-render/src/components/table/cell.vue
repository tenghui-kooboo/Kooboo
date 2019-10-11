<template>
  <td :style="{ width: width }">
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
      items: [],
      width: ""
    };
  },
  methods: {
    changeMetaId: changeMetaId
  },
  mounted() {
    if (this.meta.width != undefined) this.width = this.meta.width;
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