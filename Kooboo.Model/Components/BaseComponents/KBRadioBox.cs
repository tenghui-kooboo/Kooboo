using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.CtrlType
{
    public class KBRadioBox : BaseComponent
    {
        public override ComponentType Type => ComponentType.RadioBox;

        public List<object> Options { get; set; }

        public override VueField GetField()
        {
            var field = base.GetField();
            var dic = field.Value as Dictionary<string, object>;
            dic.Add("options", Options);
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
            var options = new List<object>();
            foreach (var attr in attrs)
            {
                if (attr is BaseAttribute)
                    continue;
                options.Add(attr);
            }

            this.Options = options;

        }
    }
}
