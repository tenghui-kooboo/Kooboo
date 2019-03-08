using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class KBTableActionBtn : IComponent
    {
        public ComponentType Type => ComponentType.KTableActionBtn;

        public IAction Action { get; set; }

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = Action.Name;
            var dic = new Dictionary<string, object>();
            dic.Add("name", Action.Name);
            dic.Add("displayName", Action.DisplayName);

            switch (Action.ActionType)
            {
                case (ButtonActionType.Link):
                    dic.Add("url", (Action as LinkAction).Url);
                    break;
                case (ButtonActionType.Modal):
                    dic.Add("modalName", (Action as ModalAction).ModalName);
                    break;
                case (ButtonActionType.Dropdown):
                    dic.Add("links", (Action as DropdownAction).Links);
                    break;
            }
            field.Value = dic;

            return field;
        }

        public bool IsValid()
        {
            return Action.IsValid();
        }
    }

}
