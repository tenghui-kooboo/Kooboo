import Vue from "vue";

export const context = new Vue();
export const states: any = {};
(window as any).__kb_context = context;
(window as any).__kb_states = states;
