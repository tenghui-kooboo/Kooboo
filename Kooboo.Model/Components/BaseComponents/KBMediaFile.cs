using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components
{
    public class KBMediaFile : BaseComponent
    {
        public override ComponentType Type => ComponentType.MediaFile;

        //todo confirm Pics's data
        public List<object> Pics { get; set; }

        public override VueField GetField()
        {
            var field = base.GetField();
            var dic = field.Value as Dictionary<string, object>;
            dic.Add("pics", Pics);
            field.Value = dic;

            return field;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override void SetData(List<Dictionary<string, List<Attribute>>> attributes)
        {
            base.SetData(attributes);
            var attrs = GetAttrs(attributes);
            var pics = new List<object>();
            foreach (var attr in attrs)
            {
                if (attr is BaseAttribute)
                    continue;
                pics.Add(attr);
            }
            this.Pics = pics;
        }
    }
}
