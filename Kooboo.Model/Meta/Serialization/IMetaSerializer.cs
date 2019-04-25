using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.Data.Context;

namespace Kooboo.Model.Meta.Serialization
{
    public interface IMetaSerializer
    {
        string Serialize(object meta, SerializationContext context);
    }

    public class SerializationContext
    {
        public RenderContext RenderContext { get; set; }
    }
}
