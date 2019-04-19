using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Setting
{
    public interface IPopup
    {
        string Title { get; set; }

        bool ShowCancelBtn { get; set; }

        string CancelBtnText { get; set; }

        List<PopupButton> Buttons { get; }
    }
}
