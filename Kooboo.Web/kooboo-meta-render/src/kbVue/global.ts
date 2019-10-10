import Vue from "vue";

const win = window as any;
export const context: Vue = win.__kb_context || new Vue();
export const componentPool: any = win.__kb_componentPool || {};
export const kInstance: any = win.__kb_kInstance || {};
win.__kb_context = context;
win.__kb_componentPool = componentPool;
win.__kb_kInstance = kInstance;
