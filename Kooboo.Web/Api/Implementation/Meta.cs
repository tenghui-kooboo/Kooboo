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

        public object Table(string model, ApiCall call)
        {
            return Get(model, "Table", call);
        }

        public object Form(string model, ApiCall call)
        {
            return Get(model, "Form", call);
        }

        public object Popup(string model, ApiCall call)
        {
            return Get(model, "Popup", call);
        }

        public object Tab(string model,ApiCall call)
        {
            return Get(model, "Tab", call);
        }

        private object Get(string modelName, string metaName, ApiCall call)
        {
            var context = new SerializationContext
            {
                RenderContext = call.Context
            };

            var str= MetaProvider.Instance.GetMeta(modelName, metaName, context);
            return Kooboo.Lib.Helper.JsonHelper.Deserialize<object>(str);
        }
    }
}
