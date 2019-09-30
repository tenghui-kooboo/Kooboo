<template>
  <div>
    <component
      :is="item.name"
      v-for="(item, index) in meta.views"
      :key="index"
      :meta="item"
    />
  </div>
</template>

<script lang="ts">
import Vue from "@/common/kbVue";
import { getData } from "@/common/api";

export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      $dataSource: null
    };
  },
  methods: {
    async loadData(url: string) {
      this.$dataSource = await getData(url);
      this.$dispath("dataLoad", {
        data: this.$dataSource
      });
    }
  }
});
</script>