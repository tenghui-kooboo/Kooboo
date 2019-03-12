using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class ApiAttribute:Attribute
    {
        public string API { get; set; }

        public ApiAttribute(string api)
        {
            API = api;
        }
    }
}
