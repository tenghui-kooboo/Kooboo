using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Kooboo.Model.Meta.Serialization
{
    public class SelectOptionsConverter : JsonConverter<SelectOptions>
    {
        public override SelectOptions ReadJson(JsonReader reader, Type objectType, SelectOptions existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, SelectOptions value, JsonSerializer serializer)
        {
            if (value.Context == null)
            {
                serializer.Serialize(writer, value.Items);
            }
            else
            {
                serializer.Serialize(writer, value.Context);
            }
        }
    }
}
