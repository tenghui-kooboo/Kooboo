using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Context;


namespace Kooboo.Model.Components.Table
{
    public class DefaultAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.None;

        public string Name { get; set; }

        public RenderContext Context { get; set; }

        public string DisplayName { get; set; }

        public string Color { get; set; }

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = this.Name;
            var dic = new Dictionary<string, object>();
            dic.Add("name", this.Name);
            dic.Add("displayName", ModelHelper.GetMultiLang(DisplayName,Context));
            dic.Add("color", this.Color);
            field.Value = dic;

            return field;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(DisplayName);
        }

        public void SetData(List<Attribute> attributes)
        {
            var attr = attributes.Find(a => a is TableActionAttribute);
            if (attr != null)
            {
                var actionAttr = attr as TableActionAttribute;
                this.Name = actionAttr.DisplayName;
                this.DisplayName = actionAttr.DisplayName;
            }
        }
    }
}
