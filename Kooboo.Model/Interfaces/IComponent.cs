using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components;
using Kooboo.Data.Context;

namespace Kooboo.Model
{
    public interface IComponent
    {
        ComponentType Type { get; }

        bool IsValid();

        VueField GetField();

        //for multi lang
        RenderContext Context { get; set; }

        // model attribute is belong to component
        bool IsMatch(Attribute attribute);

        //set data by attrs which is from model
        void SetData(List<Dictionary<string, List<Attribute>>> attributes);

        
        
    }
}
