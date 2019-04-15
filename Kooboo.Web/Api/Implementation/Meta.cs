using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Api;

namespace Kooboo.Web.Api.Implementation
{
    public class MetaApi:IApi
    {
        public string ModelName
        {
            get
            {
                return "meta";
            }
        }

        public bool RequireSite
        {
            get
            {
                return false;
            }
        }

        public bool RequireUser
        {
            get
            {
                return false;
            }
        }

        public object Get(string modelName)
        {
            return null;
        }
    }
}
