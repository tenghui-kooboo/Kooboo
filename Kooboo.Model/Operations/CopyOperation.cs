using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Operations
{
    public class CopyOperation:Attribute,IOperation
    {
        public string Name
        {
            get
            {
                return "Copy";
            }
        }
        public string DisplayName { get; set; }
    }
}
