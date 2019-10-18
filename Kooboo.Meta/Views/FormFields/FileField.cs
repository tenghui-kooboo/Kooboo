using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.FormFields
{
    public class FileField : FormField
    {
        public override string Name => nameof(FileField);

        public string Description { get; set; }
    }
}
