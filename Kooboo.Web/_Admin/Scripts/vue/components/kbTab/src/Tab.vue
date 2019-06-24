<template>
  <div>
    <ul class="nav nav-tabs">
      <li :key="key" v-for="(tab,key) in tabs" :class="{ active: key == index }">
        <a href="javascript:;" @click="onClick(tab)">{{ tab.text }}</a>
      </li>
    </ul>
  </div>
</template>
<script>
export default {
  props: {
    tabs: Array
  },
  data() {
    return {
      index: 0
    }
  },
  inject: ["ktab"],

  methods: {
    onClick(tab) {
      this.index = this.tabs.indexOf(tab)
      this.ktab.selectTab = tab
    }
  }
}
</script>

