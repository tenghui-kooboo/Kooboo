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

        public string Render(string html)
        {
            try
            {
                if(NeedGenerateJs(html))
                {
                    var doc = DomParser.CreateDom(html);
                    var js = GetJs(doc);

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
        public string GetJs(Document doc=null)
        {
            var model = GetKoobooModel(doc);
            var js = model.GetJs();
            return js;
        }

        public VueModel GetKoobooModel(Document doc)
        {
            var model = new VueModel() ;
            if (doc == null) return model;

            var modelFromHtml = new ModelFromHtml(doc);
            model.El = modelFromHtml.El;

            model.Data = GetData(modelFromHtml.DataFrom);

            var methods = modelFromHtml.Methods;
            foreach(var method in methods)
            {
                method.ApiProvider = ApiProvider;
            }
            model.Methods = methods;
            
            model.Computed = new List<string>(); //need get from html;

            model.VueCreated = GetVueCreated(modelFromHtml);


            return model;
        }

        private VueCreated GetVueCreated(ModelFromHtml modelFromHtml)
        {
            VueCreated vueCreated=null;
            if (IsApi(modelFromHtml.DataFrom))
            {
                var setting = ModelHelper.GetSetting(modelFromHtml.DataFrom, ApiProvider);

                var method = ModelApiHelper.GetMethod(modelFromHtml.DataFrom);
                if (IsTable(setting) && method != null)
                {
                    vueCreated = new VueCreated()
                    {
                        API = modelFromHtml.DataFrom,
                        ModelType = method.Class
                    };
                }

            }
            return vueCreated;
        }

        public List<VueField> GetData(string dataFrom)
        {
            //get rule by api
            //get method by api
            //get component by api

            //dataFrom="api" isReturn="true"
            //fromApi
            //returnByApi
            var dataList = new List<VueField>();

            if (IsApi(dataFrom))
            {
                var setting = ModelHelper.GetSetting(dataFrom, ApiProvider);
                if (setting != null)
                {
                    //for list/table
                    if(IsTable(setting))
                    {
                        var tableModel = ModelHelper.GetTableSetting(setting.GetType(), null);
                        if(tableModel!=null)
                        {
                            dataList.Add(tableModel.GetField());
                        }

                    }
                    else
                    {
                        //for normal
                        dataList = RuleHelper.GetVueFields(setting.GetType());
                    }
                    
                }

            }
            else
            {

            }

            return dataList;
        }

        private bool IsApi(string dataFrom)
        {
            return dataFrom.IndexOf(".") > -1;
        }
        private bool IsTable(KoobooSetting settting)
        {
            return settting is ITable;
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
