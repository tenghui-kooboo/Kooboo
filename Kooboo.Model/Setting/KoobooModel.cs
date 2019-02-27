using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model
{
    public class KoobooSetting:Attribute
    {
    }

    public class ModelNameAttribute : Attribute
    {
        public string ModelName { get; set; }

        public ModelNameAttribute(string modelName)
        {
            ModelName = modelName;
        }
    }
}
