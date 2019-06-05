using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta.Form
{
    public interface IFormMetaConfigure<TModel> : IMetaConfigure<TModel, FormMeta>
        where TModel : ISiteObject
    {
    }
}
