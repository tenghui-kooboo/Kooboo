using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Operations
{
    public class CreateOperation : Attribute,IOperation
    {
        public string Name
        {
            get
            {
                return "Create";
            }
        }
        public string DisplayName { get; set; }
    }
}
