using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta
{
    public interface IMetaConfigure<TModel, TMeta>
        where TModel : ISiteObject
        where TMeta : IViewMeta
    {
        bool IsCreator { get; }

        void Configure(TMeta meta);
    }
}
