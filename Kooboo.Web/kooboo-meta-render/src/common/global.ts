import Vue from "vue";

export const context = new Vue();
export const providers: any = {};
(window as any).__kb_context = context;
(window as any).__kb_providers = providers;
