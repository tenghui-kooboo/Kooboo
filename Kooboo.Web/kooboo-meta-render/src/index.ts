import Vue from "vue";
import "./components";
import App from "./App.vue";
new Vue({
  render: r => r(App)
}).$mount("#app");
