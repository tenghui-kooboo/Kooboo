using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta.Popup
{
    public interface IPopupMetaConfigure<TModel> : IMetaConfigure<TModel, PopupMeta>
        where TModel : ISiteObject
    {
    }
}
