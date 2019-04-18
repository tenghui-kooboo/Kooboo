using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta;

namespace Kooboo.Model.Meta.Parser
{
    public interface IMetaParser<T> where T:IKoobooModel
    {
        IKMeta Parse(T type);
    }

}
