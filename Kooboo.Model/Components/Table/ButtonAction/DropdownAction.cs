using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class DropdownAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.Link;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<LinkAction> Links { get; set; } = new List<LinkAction>();

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(DisplayName)
                && Links.Count > 0;
        }
    }
}
