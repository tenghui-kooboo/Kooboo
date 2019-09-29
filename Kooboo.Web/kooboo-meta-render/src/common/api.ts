import Axios from "axios";

export async function getMeta(modelName: string) {
  let result = await Axios.get(`/_api/meta/page?model=${modelName}`);
  return result.data.model;
}
