using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;
using Kooboo.Model.Setting;
using Kooboo.Api;
using Kooboo.Api.Methods;
using System.Reflection;
using Kooboo.Lib;
using Kooboo.Model.Helper;
using Kooboo.Dom;
using Kooboo.Data.Context;
using Kooboo.Data;

namespace Kooboo.Model
{
    public class KoobooVueRender
    {
        public string Html { get; set; }

        public object Data { get; set; }

        public IApiProvider ApiProvider { get; set; }

        public KoobooVueRender(IApiProvider apiProvider)
        {
            ApiProvider = apiProvider;
        }

        public string Render(string html,RenderContext context)
        {
            try
            {
                var url = context.Request.Path;
                if(url.StartsWith("/_Admin/Vue",StringComparison.OrdinalIgnoreCase)||
                    url.StartsWith("/_Admin/account/Vue", StringComparison.OrdinalIgnoreCase))
                {
                    var doc = DomParser.CreateDom(html);
                    var js = GetJs(doc,context);

                    var script = string.Format("<script>{0}</script>", js);
                    html += script;
                }

                return html;
            }
            catch(Exception ex)
            {

            }
            return html;
           
        }
        [Obsolete]
        public bool NeedGenerateJs(string html)
        {
            var doc = DomParser.CreateDom(html);

            var needGenerateJs = false;
            foreach (var item in doc.childNodes.item)
            {
                if (item is Comment)
                {
                    var comment = item as Comment;
                    if (comment.data != null && comment.data.Equals("vueRender", StringComparison.OrdinalIgnoreCase))
                    {
                        needGenerateJs = true;
                        break;
                    }
                }
            }
            return needGenerateJs;
        }
        public string GetJs(Document doc,RenderContext context)
        {
            //todo need cache
            var model = GetKoobooModel(doc,context);
            var js = model.GetJs();
            return js;
        }

        public VueModel GetKoobooModel(Document doc,RenderContext context)
        {
            var model = new VueModel() ;
            if (doc == null) return model;

            var modelFromHtml = new ModelFromHtml(doc);

            model.El = GetEl(modelFromHtml.El);

            model.Data = GetData(modelFromHtml.DataFrom, context);

            model.VueCreated = GetVueCreated(modelFromHtml.CreatedDataFrom, context);

            var methods = modelFromHtml.Methods;
            foreach(var method in methods)
            {
                method.ApiProvider = ApiProvider;
            }
            model.Methods = methods;
            
            model.Computed = new List<string>(); //need get from html;

            return model;
        }

        private string GetEl(string el)
        {
            //temp hardcode el id.this can be change to model setting
            return string.IsNullOrEmpty(el) ? "#id" : el;
        }
        private VueCreated GetVueCreated(string api,RenderContext context)
        {
            VueCreated vueCreated=null;
            if (string.IsNullOrEmpty(api))
            {
                var modelName = context.Request.QueryString["modelName"];
                api = ModelHelper.GetApi(modelName);
            }
            if (IsApi(api))
            {
                vueCreated = new VueCreated()
                {
                    API = api,
                    //ModelType = method.Class
                };
                //var setting = ModelHelper.GetSetting(api, ApiProvider);
                //var method = ModelApiHelper.GetMethod(api);
                //if (method != null)
                //{
                //    vueCreated = new VueCreated()
                //    {
                //        API = api,
                //        ModelType = method.Class
                //    };
                //}
            }

            return vueCreated;
        }

        public List<VueField> GetData(string dataFrom,RenderContext context)
        {
            var dataList = new List<VueField>();

            //get data from api
            if (IsApi(dataFrom))
            {
                var setting = ModelHelper.GetSetting(dataFrom, ApiProvider);
                if (setting != null)
                {
                    dataList = RuleHelper.GetVueFields(setting.GetType());
                }

            }
            else
            {
                var modelName = dataFrom;
                if (string.IsNullOrEmpty(modelName))
                {
                    modelName = context.Request.QueryString["modelName"];
                }
                if (string.IsNullOrEmpty(modelName))
                {
                    throw new Exception(Kooboo.Data.Language.Hardcoded.GetValue("can't find model", context));
                }
                //get data from model
                dataList = ModelHelper.GetVueFields(modelName, context);
            }

            return dataList;
        }

        private bool IsApi(string dataFrom)
        {
            if (string.IsNullOrEmpty(dataFrom)) return false;
            return dataFrom.IndexOf(".") > -1;
        }

        //private List<VueMethods> GetVueMethods(string html)
        //{

        //    var methods = new List<VueMethods>();
        //    methods.Add(new VueMethods()
        //    {
        //        Name = "onLogin",//can get from api
        //        Api = "Kooboo.User.Login",
        //        CallbackName = "redirect",
        //        ApiProvider=ApiProvider
        //    });

        //    return methods;
        //}
    }

    public class ApiMethod
    {
        public string Class { get; set; }

        public string Method { get; set; }

        public string Type { get; set; }
    }
}
