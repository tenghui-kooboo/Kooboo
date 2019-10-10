import { kInstance } from "@/kbVue/global";

kInstance.toList = function(obj: Object) {
  let arr = [];
  for (const key in obj) {
    arr.push({ key: key, value: obj[key as keyof typeof obj] });
  }
  return arr;
};
