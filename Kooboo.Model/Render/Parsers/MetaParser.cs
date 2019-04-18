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
            var modelName = GetModelName(el, context);

            var model = KoobooModelManager.GetMetaModel(modelName);
            if (model == null)
            {
                throw new Exception($"model {modelName} doesn't exist");
            }

            var metaKey = string.Format("{0}_{1}", modelName, Name);

            el.setAttribute(":meta", metaKey);
            context.Js.Data(metaKey, "{}");
            string url = $"/meta/get?modelname={modelName}";
            context.Js.Load(url, metaKey);

            var dataapi = GetDataApi(model);
            el.setAttribute(":data", modelName);
            context.Js.Data(modelName, "{}");
            if (!string.IsNullOrEmpty(dataapi))
            {
                context.Js.Load(dataapi, modelName);
            }

            visitChildren?.Invoke();

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
                return relation.Api;
            }
            return string.Empty;

        }

    }
}
