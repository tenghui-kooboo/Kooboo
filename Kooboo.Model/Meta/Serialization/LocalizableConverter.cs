using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Serialization
{
    public class LocalizableConverter : JsonConverter<Localizable>
    {
        public LocalizableConverter(SerializationContext context)
        {
            Context = context;
        }

        public SerializationContext Context { get; }

        public override Localizable ReadJson(JsonReader reader, Type objectType, Localizable existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Localizable value, JsonSerializer serializer)
        {
            Hardcoded.GetValue("user or ip temporarily lockout", Context.RenderContext);

            writer.WriteValue(value.Value);
        }
    }
}
