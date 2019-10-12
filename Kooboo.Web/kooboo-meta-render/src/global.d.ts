import Vue from "vue";
declare global {
  interface Window {
    __kb_context: Vue;
    __kb_componentPool: Object;
    __kb_kInstance: Object;
    Kooboo: any;
  }
}
