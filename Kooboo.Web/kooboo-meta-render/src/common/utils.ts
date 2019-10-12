import { parse } from "querystring";

export function getQuery() {
  let qs = location.search.trim();
  if (qs.startsWith("?")) qs = qs.substring(1);
  return parse(qs);
}

// export function newGuid() {
//   function S4() {
//     return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
//   }

//   return `_${S4()}${S4()}${S4()}${S4()}${S4()}${S4()}${S4()}${S4()}`;
// }

export function changeMetaId(meta: any, index: number) {
  let metaStr = JSON.stringify(meta);
  let ids: string[] = [];
  function getId(obj: any) {
    if (obj instanceof Array) {
      for (const i of obj) {
        getId(i);
      }
    } else if (obj instanceof Object) {
      for (const key in obj) {
        if (key == "id") ids.push(obj[key]);
        else getId(obj[key]);
      }
    }
  }

  getId(meta);
  for (const i of ids) {
    metaStr = metaStr.replace(RegExp(`(${i})`, "g"), `${i}_${index}`);
  }
  meta = JSON.parse(metaStr);
  // const newId = `${meta.id}_${index}`;

  // function deepReplace(obj: any, id: string) {
  //   if (obj instanceof Array) {
  //     for (const i of obj) {
  //       deepReplace(i, id);
  //     }
  //   } else if (obj instanceof Object) {
  //     for (const key in obj) {
  //       const prop = obj[key];

  //       if (key == "hooks") {
  //         const newId = `${id}_${index}`;
  //         for (const i of prop) {
  //           i.name = i.name.replace(meta.id, newId);
  //           i.execute = i.execute.replace(meta.id, newId);
  //         }
  //       } else {
  //         deepReplace(prop, id);
  //       }
  //     }
  //   }
  // }

  // deepReplace(meta, meta.id);
  // meta.id = newId;
  return meta;
}
