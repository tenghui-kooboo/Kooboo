<template>
  <div class="table-responsive">
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th v-if="showSelected">
            <input type="checkbox" v-if="list.length" @change="selectAll" >
          </th>
          <th v-for="{colName, header} in meta.columns.map(o => ({ colName: o.name, header: o.header || defaultHeaderMeta }))" :key="`header_${colName}`"
            :class="header.class" :style="header.style">{{ header.text }}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, index) in list" :key="index">
          <td v-if="showSelected"><input type="checkbox" v-model="row.__selected"></td>
          <component v-for="{colName, cell} in meta.columns.map(o => ({ colName: o.name, cell: o.cell || defaultTextMeta }))" :key="`${index}_${colName}`"
            :is="`cell-${cell.type}`" :name="colName" :meta="cell"
            :row="row" :list="list" />
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import Vue from 'vue'
import CellComponents from './cells'

const DEFAULT_HEADER_META = { text: '' }
const DEFAULT_TEXT_META = { type: 'text' }

export default {
  name: 'TableContent',

  props: {
    meta: Object,
    list: Array,
    ctx:Object,
    showSelected:{
      type:Boolean,
      default:true
    }
  },
  provide(){
    return {
      table:this
    }
  },
  computed: {
    defaultHeaderMeta () { return DEFAULT_HEADER_META },
    defaultTextMeta () { return DEFAULT_TEXT_META }
  },

  methods: {
    selectAll (e) {
      const checked = e.target.checked
      this.list.forEach(o => { Vue.set(o, '__selected', checked) })
    }
  },

  components: {
    ...CellComponents
  }
};
</script>