if (!window.Kooboo) {
    window.Kooboo = {};
}
Kooboo.Vue = {
    data: {},
    vueHooks: ["created"], //kvmodel only need support created
    getMergeHooks: function (models) {
        var mergeHooks = {};
        Kooboo.Vue.vueHooks.forEach(function (hook) {
            var hookFuncs = [];
            for (var i = models.length - 1; i >= 0; i--) {
                var model = models[i];
                if (model[hook]) {
                    hookFuncs.push(model[hook]);
                }
            }
            if (models.length > 0 && hookFuncs.length > 0) {
                //mergeHooks[hook]=hookFuncs;
                mergeHooks[hook] = function () {
                    var vm = this;
                    hookFuncs.forEach(function (hookFunc) {
                        hookFunc.call(vm);
                    });
                }
            }
        })
        return mergeHooks;
    },
    getVueData: function (data) {
        var vueData = {};
        var models = data;
        if (models) {
            var mergeHooks = Kooboo.Vue.getMergeHooks(models);
            for (var i = models.length - 1; i >= 0; i--) {
                $.extend(true, vueData, models[i]);
            }
            if (mergeHooks && Object.keys(mergeHooks).length > 0) {
                $.extend(true, vueData, mergeHooks);
            }

            if (Vue.resetValid) {
                Vue.resetValid(vueData)
            }

        }
        return vueData;
    }
}

Vue.kExtend = function (model) {
    if (model.el) {
        var models = Kooboo.Vue.data[model.el];
        if (!models) {
            models = [];
        }
        models.push(model);
        Kooboo.Vue.data[model.el] = models;
    }
}

Vue.kExecute = function () {
    var data = Kooboo.Vue.data;
    if (data && Object.keys(data).length > 0) {
        var keys = Object.keys(data);
        for (var i = 0; i < keys.length; i++) {
            var value = data[keys[i]];
            var vueData = Kooboo.Vue.getVueData(value);
            new Vue(vueData);
        }
    }

}

Vue.showMeta = function (vm, context) {
    if (context) {
        var modelName = context.parameters["modelname"];
        if (modelName) {
            var metaApi = "/meta/get?modelName=" + modelName;
            api.get(metaApi).then(function (d) {
                vm.meta = d.model;
            });

            var meta = vm.meta.form || vm.meta.table
            var loadapi = meta.loadApi;
            if (loadapi) {
                api.get(loadapi).then(function (d) {
                    vm.data = d.model;
                });
            }
            // vm.data = {
            //     name: context.selected[0].name + '_Copy',
            //     url: context.selected[0].path + '_Copy'
            // }

            vm.extra = context.context;
        }
    }

}

Object.defineProperty(Vue.prototype, "$parameterBinder", {
    get() {
        var self = this;

        function getQuerString(url) {
            var query = "";
            if (!url) {
                query = window.location.search.substr(1);
            } else {
                var questionIndex = url.indexOf("?");
                if (questionIndex > -1) {
                    query = url.substring(questionIndex + 1);
                }
            }

            var params = {};
            if (!query) return params;
            var parts = query.split("&");
            for (var i = 0; i < parts.length; i++) {
                var pair = parts[i].split("=");
                params[pair[0]] = decodeURIComponent(pair[1]);
            }

            return params;
        }

        function existSiteId(url) {
            var keyValue = getQuerString(url);
            var keys = Object.keys(keyValue);
            var index = _.findIndex(keys, function (key) {
                return key.toLowerCase() == "siteid"
            });
            return index > -1;
        }

        function addSiteId(url, model) {
            var arr = _.map(model, function (value, key) {
                return {
                    key: key.toLowerCase(),
                    value: value
                }
            });

            var pair = _.find(arr, function (pair) {
                return pair.key == "siteid"
            });
            if (pair) {
                url += url.indexOf("?") > -1 ? "&" : "?";
                url += "siteId=" + pair.value;
            }

            return url;
        }

        function getModel(model) {
            var queryStringKey = getQuerString();
            if (self.$data) {
                model = $.extend(true, {}, queryStringKey, self.$data, model);
            } else {
                model = $.extend(true, {}, queryStringKey, model);
            }
            return model;
        }
        return {
            //return url
            bind: function (url, model) {
                model = getModel(model);
                url = this.formatText(url, model);

                if (!existSiteId(url)) {
                    url= addSiteId(url, model);
                }

                return url;
                //admin/site-->admin/site?siteId=1
                //admin/site?id=1-->admin/site?id=1&siteId=1
                //admin/site?id={id},model={id:1}-->admin/site?id=1&siteId=1
                ///Development/{type}?id={id},model={type:'view',id:1}
                //-->Development/view?id=1&siteId=1
            },
            //get url keyvalue
            getKeyValue: function (url, model) {
                model = getModel(model);
                //admin/site?id={id}&name=name,model={id:1}-->{id:1,name:name}
                url = this.formatText(url, model);
                var keyvalue = getQuerString(url);

                return keyvalue;
            },
            formatText: function (text, model) {
                model = getModel(model);
                //text="aa"-->return aa
                if (!text) return "";
                
                if (text.indexOf("}") == -1 && text.indexOf("}") == -1)
                    return text;

                return text.replace(/{(\w+)}/g, function (match, key) {
                    key = key.trim();
                    if (model[key]) {
                        return model[key];
                    }
                    return "";
                });

                //text="{name}",model={name:'aa'}->return aa
                //text="{name}_copy",model={name:'aa'}-->return aa_copy
            },
            //for validation
            getValueFromModel: function (modelKey, model) {
                function getValue(data, key) {
                    if (_.findIndex(Object.keys(data), function (item) {
                            return item == key
                        }) > -1) {
                        return data[key];
                    }
                    var values = _.map(data, function (value) {
                        return value;
                    });

                    var values = _.filter(values, function (value) {
                        return value instanceof Object
                    });

                    for (var i = 0; i < values.length; i++) {
                        var value = values[i];
                        value = getValue(value, key);
                        if (value) {
                            return value;
                        }
                    }

                    return null;
                }
                model = getModel(model);
                return getValue(model, modelKey);
            },
        }
    }
})