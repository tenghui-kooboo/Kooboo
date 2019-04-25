using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.Model.Meta.Api
{
    public class PropertyInfo
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public List<Validation.ValidationRule> Rules { get; set; }
    }
}
