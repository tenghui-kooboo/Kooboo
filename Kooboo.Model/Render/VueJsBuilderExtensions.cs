using System;
using System.Collections.Generic;

using Kooboo.Model.Render.Vue;

namespace Kooboo.Model.Render
{
    public static class VueJsBuilderExtensions
    {
        public static IJsBuilder Hook(this IJsBuilder builder, string name, string body)
        {
            return builder.AddItem(new Vue.Hook { Name = name, Body = body });
        }

        public static IJsBuilder Data(this IJsBuilder builder, string name, string body)
        {
            return builder.AddItem(new Vue.Data { Name = name, Body = body });
        }

        public static IJsBuilder Method(this IJsBuilder builder, string name, string body)
        {
            return builder.AddItem(new Vue.Method { Name = name, Body = body });
        }

        public static IJsBuilder Validation(this IJsBuilder builder,string name,List<ValidateRules.Rule> rules)
        {
            return builder.AddItem(new Validation { Name=name,Rules=rules});
        }

        public static IJsBuilder El(this IJsBuilder builder, string name)
        {
            return builder.AddItem(new Vue.El { Name = name });
        }
    }
}
