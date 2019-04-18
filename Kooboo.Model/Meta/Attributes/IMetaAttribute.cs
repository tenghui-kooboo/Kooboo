using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    public interface IMetaAttribute
    {
        bool IsHeader { get; }
        string PropertyName { get; }

        object Value();
    }
}
