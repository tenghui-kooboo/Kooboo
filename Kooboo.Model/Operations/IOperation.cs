using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Operations
{
    public interface IOperation
    {
        string Name { get; }

        string DisplayName{get;set;}

        
    }

}
