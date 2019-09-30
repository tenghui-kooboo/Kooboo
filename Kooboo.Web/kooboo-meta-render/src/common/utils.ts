import { parse } from "querystring";

export function getQuery() {
  let qs = location.search.trim();
  if (qs.startsWith("?")) qs = qs.substring(1);
  return parse(qs);
}
