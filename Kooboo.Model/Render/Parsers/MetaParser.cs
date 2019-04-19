using System;
using Kooboo.Dom;
using Kooboo.Model.Meta.Attributes;
using System.Reflection;
using System.Linq;

namespace Kooboo.Model.Render.Parsers
{
    public class MetaParser : IVirtualElementParser
    {
        public string Name => "meta";

        public int Priority => ParserPriority.High;

        public void Parse(Element el, TagParseContext context, Action visitChildren)
        {
            if (context.ViewContext.ViewType == ViewType.Sub)
            {
                var dataName = "data";
                var metaName = "meta";
                //el.setAttribute($":{metaName}", $"{metaName}");
                //el.setAttribute($":{dataName}", $"{dataName}");
                

                context.Js.Data("showModal", "false");
                context.Js.Data(dataName, "{}");
                context.Js.Data(metaName, "{popup:{},form:{}}");
                context.Js.Data("extra", "[]");
                //only use to trigger load function
                context.Js.Load("", "data");
                visitChildren?.Invoke();
                return;
            }

            var modelName = GetModelName(el, context);

            var model = KoobooModelManager.GetMetaModel(modelName);
            if (model == null)
            {
                throw new Exception($"model {modelName} doesn't exist");
            }

            var metaKey = string.Format("{0}_{1}", modelName, Name);

            //el.setAttribute(":meta", metaKey);
            //context.Js.Data(metaKey, "{}");
            //string url = $"/meta/get?modelname={modelName}";
            //context.Js.Load(url, metaKey);
            SetLoadMeta(el,context,modelName,metaKey);

            var dataapi = GetDataApi(model);
            el.setAttribute(":data", modelName);
            context.Js.Data(modelName, "{}");
            if (!string.IsNullOrEmpty(dataapi))
            {
                context.Js.Load(dataapi, modelName);
            }

            visitChildren?.Invoke();

        }

        private void SetLoadMeta(Element el,TagParseContext context,string modelName,string metaKey)
        {
            el.setAttribute(":meta", metaKey);
            context.Js.Data(metaKey, "{}");
            string url = $"/meta/get?modelname={modelName}";
            context.Js.Load(url, metaKey);
        }

        private bool IsDynamicMeta(string modelName)
        {
            return modelName.IndexOf("{") > -1 && modelName.IndexOf("}") > -1;
        }
        private string GetModelName(Element el,TagParseContext context)
        {
            var modelName = el.getAttribute(context.Options.GetAttributeName(Name));
            if (IsDynamicMeta(modelName))
            {
                modelName = modelName.Replace("{", "").Replace("}", "").Trim();
                if(context.ViewContext.Context!=null)
                    modelName = context.ViewContext.Context.Request.Get(modelName);
            }

            return modelName;
        }

        private string GetDataApi(IKoobooModel model)
        {
            var relation = model.GetType().GetCustomAttributes(typeof(RelationAttribute)).ToList()
                            .Select(a => a as RelationAttribute).FirstOrDefault();
            if (relation != null)
            {
                return relation.LoadDataApi;
            }
            return string.Empty;

        }

    }
}
