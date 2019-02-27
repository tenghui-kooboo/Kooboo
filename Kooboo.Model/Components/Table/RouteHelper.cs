using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public class RouteHelper
    {
        public static string GetUrl(RenderContext context,string relativeUrl,string param,bool withSiteId=true)
        {
            //todo modify
            var url = "/_Admin/" + relativeUrl;
            var siteId = context.Request.GetValue("siteId");
            if(withSiteId && !string.IsNullOrEmpty(siteId))
            {
                url += "?SiteId=" + siteId;
            }
            url += url.IndexOf("?") > -1 ? "&" : "?";

            url += param;
            return url;

        }
    }
}
