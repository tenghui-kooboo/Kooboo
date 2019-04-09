using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;

namespace Kooboo.Model.Render.Parsers
{
    public class SubmitParser : IVirtualElementParser
    {
        public const string ModelAttribute = "model";
        public const string Data_ModelName = "modelName";
        public const string Data_Url = "url";

        public string Name => "submit";

        public int Priority => ParserPriority.Normal;

        public void Parse(Element el, TagParseContext context, Action visitChildren)
        {
            var url = el.getAttribute(context.Options.GetAttributeName(Name));
            var modelName = el.getAttribute(context.Options.GetAttributeName(ModelAttribute)) ?? ParserHelper.GetModelNameFromUrl(url);
            context.Set(Data_Url, url);
            context.Set(Data_ModelName, modelName);

            // Get meta from url
            string[] paramNames = null;
            Dictionary<string, Type> properties = null;
            Dictionary<string, List<ValidateRules.Rule>> rules = null;

            // action attribute
            var urlWithParams = ParserHelper.GenerateUrlFromApiParameters(url, paramNames);
            el.setAttribute("action", urlWithParams);

            // method attributes
            if (String.IsNullOrEmpty(el.getAttribute("method")))
            {
                el.setAttribute("method", "post");
            }

            // @submit.native
            el.setAttribute("@submit.native", $"{Vue.SubmitData.Keyword_Submit}_{modelName}");

            // data model
            var json = String.Join(",", properties.Select(o => String.Format($"{o.Key}: {ParserHelper.GetDefaultValueFromType(o.Value)}")));
            context.Js.Data(modelName, json);

            // submit method
            context.Js.Submit(urlWithParams, modelName);

            // validations
            foreach (var rule in rules)
            {
                context.Js.Validation(rule.Key, rule.Value);
            }

            visitChildren();
        }
    }
}
