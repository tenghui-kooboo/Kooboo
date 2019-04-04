using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Operations
{
    public class DeleteOperation:Attribute,IOperation
    {
        public string Name
        {
            get
            {
                return "Delete";
            }
        }
        public string DisplayName { get; set; }
    }
}
