using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components;

namespace Kooboo.Model
{
    public interface IComponent
    {
        ComponentType Type { get; }

        bool IsValid();

        VueField GetField();
        
    }
}
