function getAPI(){
  var url="/user/get";
  var apiObj=api.getApi(url);
  expect(apiObj).not.to.be(undefined);
  expect(apiObj.obj).to.be("user");
  expect(apiObj.method).to.be("get");

  url="user/get";
  var apiObj=api.getApi(url);
  expect(apiObj).not.to.be(undefined);
  expect(apiObj.obj).to.be("user");
  expect(apiObj.method).to.be("get");

  url="user/get?id=1&data=d";
  var apiObj=api.getApi(url);
  expect(apiObj).not.to.be(undefined);
  expect(apiObj.obj).to.be("user");
  expect(apiObj.method).to.be("get");
  debugger;
  expect(apiObj.data.id).to.be("1");
  expect(apiObj.data.data).to.be("d");

}
