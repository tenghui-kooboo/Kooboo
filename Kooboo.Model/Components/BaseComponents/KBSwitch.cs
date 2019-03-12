using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.CtrlType
{
    public class KBSwitch : BaseComponent
    {
        public override ComponentType Type => ComponentType.Switch;

        public List<object> Validations { get; set; }

        public override VueField GetField()
        {
            var field = base.GetField();
            var dic = field.Value as Dictionary<string, object>;
            dic.Add("validations", Validations);
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
            var validations = new List<object>();
            foreach (var attr in attrs)
            {
                if (attr is BaseAttribute)
                    continue;
                validations.Add(attr);
            }

            this.Validations = validations;

        }
    }
}
