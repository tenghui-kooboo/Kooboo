using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kooboo.Model.Meta.Serialization
{
    public partial class JsonNetSerializer : IMetaSerializer
    {
        public static Action<JsonSerializerSettings, SerializationContext> ConfigureSettings { get; set; }

        public string Serialize(object meta, SerializationContext context)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            settings.Converters.Add(new LocalizableConverter(context));
            settings.Converters.Add(new FormatConverter());
            settings.Converters.Add(new SelectOptionsConverter());

            ConfigureSettings?.Invoke(settings, context);

            return JsonConvert.SerializeObject(meta, settings);
        }
    }
}
