using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class LinkAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.Link;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Url { get; set; }

        public string Color { get; set; }

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = this.Name;

            var dic = new Dictionary<string, object>();
            dic.Add("name", this.Name);
            dic.Add("displayName", this.DisplayName);
            dic.Add("color", this.Color);
            dic.Add("url", this.Url);
            field.Value = dic;

            return field;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(DisplayName);
        }

        public void SetData(List<Attribute> attributes)
        {
            var linkAttr = attributes.Find(a => a is Kooboo.Model.Attributes.LinkAttribute) as Attributes.LinkAttribute;
            if (linkAttr != null)
            {
                this.Url = linkAttr.Url;
                this.DisplayName = linkAttr.FieldName;//to do change 
            }
            
        }
    }
}
