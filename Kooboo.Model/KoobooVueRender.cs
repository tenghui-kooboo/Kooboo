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

            model.Data = GetDataList(modelFromHtml.DataFrom);

            var methods = modelFromHtml.Methods;
            foreach(var method in methods)
            {
                method.ApiProvider = ApiProvider;
            }
            model.Methods = methods;
            
            model.Computed = new List<string>(); //need get from html;
            
            return model;
        }
        

        public List<VueField> GetDataList(string dataFrom)
        {
            //dataFrom="api" isReturn="true"
            //fromApi
            //returnByApi
            var dataList = new List<VueField>();
            if (IsApi(dataFrom))
            {
                var ruleSetting = RuleHelper.GetRuleSettingByApi(dataFrom, ApiProvider);
                if (ruleSetting != null)
                {
                    dataList =RuleHelper.GetVueFields(ruleSetting.GetType());
                }

            }
            else
            {

            }

            return dataList;
        }

        public bool IsApi(string dataFrom)
        {
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
