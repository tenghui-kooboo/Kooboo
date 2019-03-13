using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public class ModalAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.Modal;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Color { get; set; }

        public string ModalName { get; set; }

        public RenderContext Context { get; set; }

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = this.Name;

            var dic = new Dictionary<string, object>();
            dic.Add("name", this.Name);
            dic.Add("displayName", ModelHelper.GetMultiLang(DisplayName,Context));
            dic.Add("color", this.Color);
            dic.Add("modalName", this.ModalName);
            field.Value = dic;

            return field;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ModalName) && !string.IsNullOrEmpty(DisplayName);
        }

        public void SetData(List<Attribute> attributes)
        {
            var modalAttr= attributes.Find(a => a is Kooboo.Model.Attributes.ModalAttribute) as Attributes.ModalAttribute;
            if (modalAttr != null)
            {
                ModalName = modalAttr.ModalName;
            }
        }
    }
}
