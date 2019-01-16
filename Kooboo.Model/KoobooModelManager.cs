using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Helper;
using Kooboo.Api;

namespace Kooboo.Model
{
    public class KoobooModelManager
    {
        private static KoobooVueRender KoobooVueRender;
        public static void InitProvider(IApiProvider provider)
        {
            KoobooVueRender = new KoobooVueRender(provider);
        }

        public static string Render(string html)
        {
            return KoobooVueRender.Render(html);
        }
    }
}
