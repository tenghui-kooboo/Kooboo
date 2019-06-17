using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta
{
    public interface IMetaConfigure<TModel, TMeta>
        where TModel : ISiteObject
        where TMeta : IViewMeta
    {
        void Configure(TMeta meta);
    }
}
