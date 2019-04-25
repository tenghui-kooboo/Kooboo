using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public interface IMetaCreator<TModel, TMeta> : IMetaConfigure<TModel, TMeta>
        where TMeta : IViewMeta
    {
    }
}
