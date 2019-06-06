using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kooboo.Model.Meta.Serialization
{
    public class FormatConverter : JsonConverter<Format>
    {
        public override Format ReadJson(JsonReader reader, Type objectType, Format existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Format value, JsonSerializer serializer)
        {
            if (value.Condition == null)
            {
                writer.WriteValue(value.Text.Value);
            }
            else
            {
                serializer.Serialize(writer, value.Condition);
            }
        }
    }
}
