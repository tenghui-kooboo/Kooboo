import Vue from "vue";

window.__kb_context = window.__kb_context || new Vue();
window.__kb_componentPool = window.__kb_componentPool || {};
window.__kb_kInstance = window.__kb_kInstance || {};

export const context: Vue = window.__kb_context;
export const componentPool: any = window.__kb_componentPool;
export const kInstance: any = window.__kb_kInstance;
