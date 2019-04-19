using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kooboo.Model.Setting;

namespace Kooboo.Model.Meta.Definition
{
    public class KFormMeta:IKMeta
    {
        public IPopup Popup { get; set; }

        public KForm Form { get; set; }
        
    }

}
