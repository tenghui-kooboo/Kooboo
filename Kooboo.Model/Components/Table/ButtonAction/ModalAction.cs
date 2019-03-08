using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class ModalAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.Link;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ModalName { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ModalName) && !string.IsNullOrEmpty(DisplayName);
        }
    }
}
