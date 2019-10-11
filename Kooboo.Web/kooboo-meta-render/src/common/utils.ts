import { parse } from "querystring";

export function getQuery() {
  let qs = location.search.trim();
  if (qs.startsWith("?")) qs = qs.substring(1);
  return parse(qs);
}

export function newGuid() {
  function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
  }

  return `_${S4()}${S4()}${S4()}${S4()}${S4()}${S4()}${S4()}${S4()}`;
}

export function changeMetaId(meta: any) {
  meta = JSON.parse(JSON.stringify(meta));
  const guid = newGuid();

  function deepReplace(obj: any) {
    if (obj instanceof Array) {
      for (const i of obj) {
        deepReplace(i);
      }
    } else if (obj instanceof Object) {
      for (const key in obj) {
        const prop = obj[key];

        if (key == "hooks") {
          for (const i of prop) {
            i.name = i.name.replace(meta.id, guid);
            i.execute = i.execute.replace(meta.id, guid);
          }
        } else {
          deepReplace(prop);
        }
      }
    }
  }

  deepReplace(meta);
  meta.id = guid;
  console.log(meta);
  return meta;
}
