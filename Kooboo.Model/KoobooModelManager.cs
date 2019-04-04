using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Api;
using Kooboo.Data.Context;
using Kooboo.Model.Render;
namespace Kooboo.Model
{
    public class KoobooModelManager
    {

        private static ViewParseOptions options;
        public static void InitProvider(IApiProvider provider)
        {
            options = new ViewParseOptions() { ApiProvider = provider };
            foreach (var item in Lib.Reflection.AssemblyLoader.LoadTypeByInterface(typeof(IVirtualElementParser)))
            {
                var instance = Activator.CreateInstance(item) as IVirtualElementParser;

                if (instance != null)
                {
                    options.ElementParsers.Add(instance.Name, instance);
                }
            }
        }

        public static string Render(string html,RenderContext context)
        {
            if (!IsNeedRender(context))
                return html;
            var jsBuilder = new VueJsBuilder();
            new ViewParser(options).Parse(html, jsBuilder);
            var js= jsBuilder.Build();
            var script = string.Format("<script>{0}</script>", js);
            html += script;
            
            return html;
        }
        private static bool IsNeedRender(RenderContext context)
        {
            var url = context.Request.Path;
            return url.StartsWith("/_Admin/Vue", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("/_Admin/account/Vue", StringComparison.OrdinalIgnoreCase);
           
        }
    }
}
