<template>
  <tr>
    <td class="table-checkbox" v-if="showCheck">
      <input
        type="checkbox"
        v-model="checked"
        @input="onCheck($event.target.checked)"
      />
    </td>
    <table-cell
      v-for="(cell, index) in meta.cells"
      :key="index"
      :meta="changeMetaId(cell)"
    />
  </tr>
</template>

<script lang="ts">
import Vue from "@/kbVue";
import TableCell from "./cell.vue";
import { changeMetaId } from "@/common/utils";

export default Vue.extend({
  props: {
    showCheck: Boolean,
    data: Object,
    checkedAll: Boolean,
    meta: Object
  },
  mounted(){
    this.$dispath("dataChange", { data: this.data });
  },
  data() {
    return {
      checked: false
    };
  },
  watch: {
    checkedAll(checked: boolean) {
      this.checked = checked;
    },
    data() {
      this.$dispath("dataChange", { data: this.data });
    }
  },
  methods: {
    changeMetaId: changeMetaId,
    onCheck(checked: boolean) {
      this.$emit("checked", checked);
    },
    itemMeta(cell: any, index: number) {
      let meta = JSON.parse(JSON.stringify(cell));
      meta.id += `_${index}`;
      for (const i of meta.hooks) {
        i.name += `_${index}`;
      }
      return meta;
    }
  },
  components: {
    TableCell
  }
});
</script>