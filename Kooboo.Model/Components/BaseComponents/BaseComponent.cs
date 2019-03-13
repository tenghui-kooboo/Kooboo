using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components
{
    public class BaseComponent : IComponent
    {
        public virtual ComponentType Type => ComponentType.None;

        public bool IsMultilingual { get; set; }

        public string FieldName { get; set; }

        public bool Disabled { get; set; }

        public object FieldValue { get; set; }

        public string Tooltip { get; set; }

        public RenderContext Context { get; set; }

        public virtual VueField GetField()
        {
            var field = new VueField();
            field.Name = FieldName;

            var dic = new Dictionary<string, object>();
            //todo confirm multi lang
            dic.Add("isMultilingual", IsMultilingual);
            dic.Add("fieldName", FieldName);
            dic.Add("disabled", Disabled);
            dic.Add("fieldValue", FieldValue);
            dic.Add("tooltip", Tooltip);
            field.Value = dic;

            return field;
        }

        public virtual bool IsMatch(Attribute attribute)
        {
            var attr = attribute as BaseAttribute;
            if (attr == null) return false;
            return attr.ComponentType==Type;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        public virtual void SetData(List<Dictionary<string, List<Attribute>>> attributes)
        {
            var attrs = GetAttrs(attributes);
            var attr = attrs.Find(a => a is BaseAttribute);
            if(attr != null)
            {
                var baseAttr = attr as BaseAttribute;
                IsMultilingual = baseAttr.IsMultilingual;
                Tooltip = baseAttr.Tooltip;
                FieldValue = baseAttr.FieldValue;
                Disabled = baseAttr.Disabled;
            }
        }


        //todo to be optimized
        protected List<Attribute> GetAttrs(List<Dictionary<string, List<Attribute>>> attributes)
        {
            var list = new List<Attribute>();
            if (attributes.Count > 0)
            {
                var attrDic = attributes[0];
                foreach (var keyPair in attrDic)
                {
                    this.FieldName = keyPair.Key;
                    list = keyPair.Value;
                    break;
                }
            }
            return list;
        }
    }
}
