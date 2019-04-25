using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class RemoteRule : ValidationRule
    {
        public RemoteRule(string api)
        {
            Api = api;
        }

        public string Api { get; set; }
    }
}
