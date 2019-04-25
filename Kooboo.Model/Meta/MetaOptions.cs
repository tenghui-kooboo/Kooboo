using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kooboo.Model.Meta.Form;

namespace Kooboo.Model.Meta
{
    public class MetaOptions
    {
        public static MetaOptions Instance { get; set; } = new MetaOptions();

        public FormMetaOptions Form { get; } = FormMetaOptions.Default();
    }
}
