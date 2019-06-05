using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta.Table
{
    public interface ITableMetaConfigure<TModel> : IMetaConfigure<TModel, TableMeta>
        where TModel : ISiteObject
    {
    }
}
