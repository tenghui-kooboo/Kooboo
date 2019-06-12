function parameterBindTest(){
  var vue=new Vue({
    data:{
      id:1
    }
  })
  var model={
      type:"view",
  }
  var url="admin/site";
  
  url=vue.$parameterBinder.bind(url,model);
  expect(url).to.be("admin/site");

  url="admin/site?id=1"
  url=vue.$parameterBinder.bind(url,model);
  expect(url).to.be("admin/site?id=1");

  url="admin/site?id={id}"
  url=vue.$parameterBinder.bind(url,model);
  expect(url).to.be("admin/site?id=1");

  url="Development/{type}?id={id}"
  url=vue.$parameterBinder.bind(url,model);
  expect(url).to.be("Development/view?id=1");
}

function getKeyvalueTest(){
  var vue=new Vue({
    data:{
      id:1
    }
  })
  var model={
      type:"view",
  }

  url="Development/{type}?id={id}"
  var keyvalue=vue.$parameterBinder.getKeyValue(url,model);
  expect(keyvalue["id"]).to.be("1");

  url="Development/{type}?id={id}&type={type}"
  var keyvalue=vue.$parameterBinder.getKeyValue(url,model);
  expect(keyvalue["id"]).to.be("1");
  
  expect(keyvalue["type"]).to.be("view");
}

function formatText(){
  var vue=new Vue({
    data:{
      name:"1"
    }
  })
  var model={
      type:"view",
  }
  var text="name";
  text=vue.$parameterBinder.formatText(text);
  expect(text).to.be("name");

  text="{name}";
  text=vue.$parameterBinder.formatText(text);
  expect(text).to.be("1");

  text="{name}_copy";
  text=vue.$parameterBinder.formatText(text);
  expect(text).to.be("1_copy");

  text="{type}_copy";
  text=vue.$parameterBinder.formatText(text,model);
  expect(text).to.be("view_copy");
}

function getValueFromModel_test()
{
  var vue=new Vue({
    data:{
      user:{
        name:'1'
      }
    }
  })
  var text=vue.$parameterBinder.getValueFromModel("name");
  expect(text).to.be("1");

}