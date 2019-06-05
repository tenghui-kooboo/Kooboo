using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Configure
{
    public interface IMetaConfigureCollector
    {
        bool Initialized { get; }

        void Initialize(Action<ConfiugreFoundCallback> onConfigureFound);

        IViewMeta CreateMeta(Type modelType, Type metaType);
    }

    public class ConfiugreFoundCallback
    {
        public Type ModelType { get; set; }

        public Type MetaType { get; set; }
    }
}
