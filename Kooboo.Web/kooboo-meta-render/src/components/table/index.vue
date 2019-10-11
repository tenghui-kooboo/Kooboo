<template>
  <table class="table table-striped table-hover">
    <thead>
      <tr>
        <th v-if="meta.showCheck" class="table-checkbox">
          <input
            type="checkbox"
            v-model="checked"
            @input="onChecked($event.target.checked)"
          />
        </th>
        <th v-for="(item, index) in meta.rowTemplate.cells" :key="index">
          {{ item.text }}
        </th>
      </tr>
    </thead>

    <tbody>
      <table-row
        v-for="(item, index) in items"
        :key="index"
        :showCheck="meta.showCheck"
        :meta="changeMetaId(meta.rowTemplate)"
        :data="item"
        :checkedAll="checkedAll"
        @checked="rowChecked($event, item)"
      />
    </tbody>
  </table>
</template>

<script lang="ts">
import Vue from "@/kbVue";
import TableRow from "./row.vue";
import {changeMetaId} from "@/common/utils"

export default Vue.extend({
  props: {
    meta: Object
  },
  data() {
    return {
      items: [],
      checkedAll: false,
      checked: false,
      selectedRows: [] as any[]
    };
  },
  methods: {
    changeMetaId: changeMetaId,
    rowChecked(checked: boolean, item: any) {
      if (checked) {
        this.selectedRows = this.selectedRows.concat([item]);
      } else {
        this.selectedRows = this.selectedRows.filter(f => f != item);
      }

      this.checked = this.selectedRows.length == this.items.length;
    },
    onChecked(checked: boolean) {
      this.checkedAll = !this.checkedAll;
      this.$nextTick(() => {
        this.checkedAll = checked;
        this.selectedRows = checked ? this.items : [];
      });
    }
  },
  watch: {
    selectedRows() {
      this.$dispath("selected_rows_change", {
        selectedRows: this.selectedRows
      });
    }
  },
  components: {
    TableRow
  }
});
</script>