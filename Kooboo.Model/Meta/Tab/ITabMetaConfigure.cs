using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta.Tab
{

    public interface ITabMetaConfigure<TModel> : IMetaConfigure<TModel, TabMeta>
        where TModel : ISiteObject
    {
    }
}
