import Vue from "vue";
import "./components";
import App from "./App.vue";
import { getMeta } from "@/common/api";
import "@/common/kExtends";

(async function() {
  const root = document.getElementById("app");
  if (!root) return console.log("Not element id is app");
  const modelName = root.getAttribute("model-name");
  if (!modelName) return console.log("app element not attribute model-name");
  const meta = await getMeta(modelName);

  new Vue({
    render: r => r(App, { props: { meta: meta } })
  }).$mount("#app");
})();
