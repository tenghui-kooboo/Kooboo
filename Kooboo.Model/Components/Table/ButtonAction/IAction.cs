using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public interface IAction
    {
        string Name { get; set; }
        string DisplayName { get; set; }
        ButtonActionType ActionType { get; }
        bool IsValid();
    }
}
