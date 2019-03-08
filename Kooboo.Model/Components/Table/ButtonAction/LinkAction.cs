using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class LinkAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.Link;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Url { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(DisplayName);
        }
    }
}
