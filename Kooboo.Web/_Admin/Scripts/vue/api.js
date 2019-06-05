var loading = {
  requestCount: 0,
  start: function () {
    this.requestCount++;
    $(".page-loading").show();
  },
  stop: function () {
    this.requestCount--;
    if (this.requestCount === 0) {
      $(".page-loading").hide();
    }
  },
  initial: function () {
    if (this.requestCount === 0) {
      $(".page-loading").hide();
    }
  }
};

api = {
  get: function (url,hideLoading) {
    var apiObj = this.getApi(url);
    if (!apiObj) return null;
    var self = this;
    self.startLoading(hideLoading);

    return DataCache.getData(apiObj.obj, apiObj.method, apiObj.data, true)
      .fail(function (fail) {
        self.handleRequestError(fail);
      })
      .always(function () {
        self.stopLoading(hideLoading);
      })
      .done(function (res) {
        if (!res.success) {
          Kooboo.handleFailMessages(res.messages);
        }
      });
  },
  // getAsync:function(url,useSnyc,hideLoading){
  //   var apiObj = this.getApi(url);
  //   if (!apiObj) return null;
  //   var self = this;
  //   self.startLoading(hideLoading);

  //   return DataCache.getData(apiObj.obj, apiObj.method, apiObj.data, useSnyc)
  //     .fail(function (fail) {
  //       self.handleRequestError(fail);
  //     })
  //     .always(function () {
  //       self.stopLoading(hideLoading);
  //     })
  //     .done(function (res) {
  //       if (!res.success) {
  //         Kooboo.handleFailMessages(res.messages);
  //       }
  //     });
  // },
  post: function (url, model,hideLoading) {
    var apiObj = this.getApi(url);
    if (!apiObj) return null;
    var self = this;
    self.startLoading(hideLoading);

    if (model instanceof Object) {
      model = JSON.stringify(model);
    }
    model = encodeURIComponent(model);
    return DataCache.postData(apiObj.obj, apiObj.method, model, {}, true)
      .fail(function (fail) {
        self.handleRequestError(fail);
      })
      .always(function () {
        self.stopLoading(hideLoading);
      })
      .done(function (res) {
        // clean cache...
        if (!res.success) {
          Kooboo.handleFailMessages(res.messages);
        } else {
          // if(res.model&& res.model.redirectUrl){
          //   window.location.href=res.model.redirectUrl;
          //todo 
          // if (res.model) {
          //   window.location.href = res.model;
          // } else {
          //   window.location.href = window.location.href;
          // }

          window.location.href = window.location.href;
        }
      });
  },
  upload: function (url, model, progressor) {
    var self = this;
    var apiObj = this.getApi(url);
    if (!apiObj) return null;

    !progressor && loading.start();

    return DataCache.uploadData(apiObj.obj, apiObj.method, model, progressor)
      .fail(function (fail) {
        self.handleRequestError(fail);
      })
      .always(function () {
        !progressor && loading.stop();
      })
      .done(function (res) {
        if (!res.success) {
          Kooboo.handleFailMessages(res.messages);
        }
      });
  },

  // getMeta: function (modelName) {
  //   var url = "/meta/get?modelname=" + modelName;
  //   var meta = {};
  //   this.get(url).then(function (res) {
  //     if (res.model) {
  //       meta = res.model;
  //     }
  //   })
  //   return meta;
  // },
  tableMeta:function(modelName){
    var url = "/meta/table?model=" + modelName;
    var meta = {};
    this.get(url).then(function (res) {
      if (res.model) {
        meta = res.model;
      }
    })
    return meta;
  },
  popupMeta:function(modelName){
    var url = "/meta/popup?model=" + modelName;
    var meta = {};
    this.get(url).then(function (res) {
      if (res.model) {
        meta = res.model;
      }
    })
    return meta;
  },
  formMeta:function(modelName){
    var url = "/meta/form?model=" + modelName;
    var meta = {};
    this.get(url).then(function (res) {
      if (res.model) {
        meta = res.model;
      }
    })
    return meta;
  },
  getApi: function (url) {
    var slashParts = url.split("/");

    var apiParts = [];
    for (var i = 0; i < slashParts.length; i++) {
      var part = slashParts[i];
      if (part) {
        apiParts.push(part);
      }
    }
    if (apiParts.length < 2) {
      console.log("api is wrong");
      return null;
    }
    return {
      obj: apiParts[0],
      method: apiParts[1].split("?")[0],
      data: this.getData(url)
    }

  },
  getData: function (url) {
    parts = url.split("?");
    var data = {};
    if (parts.length > 1) {
      var queryStringParts = parts[1].split("&");
      for (var i = 0; i < queryStringParts.length; i++) {
        var part = queryStringParts[i];
        var itemParts = part.split("=");
        if (itemParts.length == 2) {
          if (itemParts[1]) {
            data[itemParts[0]] = itemParts[1];
          }
        }
      }
    }
    return data;
  },
  startLoading: function (hideLoading) {
    var hideLoading = !!hideLoading && true;
    hideLoading && $(".page-loading").hide();
    !hideLoading && loading.start();
  },
  stopLoading: function (hideLoading) {
    !hideLoading && loading.stop();
  },
  handleRequestError: function () {
    window.info.fail(
      Kooboo.text.info.networkError + ", " + Kooboo.text.info.checkServer
    );
  },
  getList: function (modelName) {
    var meta = this.tableMeta(modelName);

    var data = {};
    if (meta.dataApi) {
      api.get(meta.dataApi)
        .then(function (res) {
          data = res.model || res;
        });
    }

    return {
      meta: meta,
      data: data,
      popup: {}
    }
  },

}