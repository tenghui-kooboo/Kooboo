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
      v-for="(column, index) in columns"
      :key="index"
      :meta="column"
    />
  </tr>
</template>

<script lang="ts">
import Vue from "vue";
import TableCell from "./cell.vue";

export default Vue.extend({
  props: {
    showCheck: Boolean,
    data: Object,
    columns: Array,
    checkedAll: Boolean
  },
  data() {
    return {
      checked: false
    };
  },
  watch: {
    checkedAll(checked: boolean) {
      this.checked = checked;
    }
  },
  methods: {
    onCheck(checked: boolean) {
      this.$emit("checked", checked);
    }
  },
  components: {
    TableCell
  }
});
</script>