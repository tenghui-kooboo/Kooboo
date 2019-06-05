using System;
using System.Collections.Generic;
using System.Text;

using Kooboo.Api;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Serialization;

namespace Kooboo.Web.Api.Implementation
{
    public class MetaApi : IApi
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

        public string Table(string model, ApiCall call)
        {
            return Get(model, "Table", call);
        }

        public string Form(string model, ApiCall call)
        {
            return Get(model, "Form", call);
        }

        public string Popup(string model, ApiCall call)
        {
            return Get(model, "Popup", call);
        }

        private string Get(string modelName, string metaName, ApiCall call)
        {
            var context = new SerializationContext
            {
                RenderContext = call.Context
            };

            return MetaProvider.Instance.GetMeta(modelName, metaName, context);
        }
    }
}
