using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;

namespace Kooboo.Model.Render.Parsers
{
    public class LoadParser : IVirtualElementParser
    {
        public string Name => "load";

        public int Priority => ParserPriority.Low;

        public void Parse(Element el, TagParseContext context, Action visitChildren)
        {
            // Get url
            context.TryGet<string>(SubmitParser.Data_Url, out string url);
            url = url ?? el.getAttribute(context.Options.GetAttributeName(Name));

            // Get modelName
            var modelGenerated = context.TryGet<string>(SubmitParser.Data_ModelName, out string modelName);
            modelName = modelName ??
                el.getAttribute(context.Options.GetAttributeName(SubmitParser.ModelAttribute)) ?? 
                ParserHelper.GetModelNameFromUrl(url);

            // Get meta from url
            string[] paramNames = null;
            Dictionary<string, Type> properties = null;

            // data model
            if (!modelGenerated)
            {
                var json = String.Join(",", properties.Select(o => String.Format($"{o.Key}: {ParserHelper.GetDefaultValueFromType(o.Value)}")));
                context.Js.Data(modelName, json);
            }

            // load js
            var urlWithParams = ParserHelper.GenerateUrlFromApiParameters(url, paramNames);
            context.Js.Load(urlWithParams, modelName);

            visitChildren();
        }
    }
}
