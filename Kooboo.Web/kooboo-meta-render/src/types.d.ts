import Vue from "vue";

declare module "*.vue" {
  export default Vue;
}

declare module "*.json" {
  const json: any;
  export default json;
}

declare global {
  interface Window {
    __kb_context: Vue;
    __kb_componentPool: Object;
    __kb_kInstance: Object;
    Kooboo: any;
  }
}