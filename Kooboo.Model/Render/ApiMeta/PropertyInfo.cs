using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Meta.Validation;

namespace Kooboo.Model.Render.ApiMeta
{
    public class PropertyInfo
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public List<ValidationRule> Rules { get; set; }
    }
}
