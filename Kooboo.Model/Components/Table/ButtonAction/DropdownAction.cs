using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.Table
{
    public class DropdownAction : IAction
    {
        public ButtonActionType ActionType => ButtonActionType.Dropdown;

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<LinkAction> Links { get; set; } = new List<LinkAction>();

        public string Color { get; set; }

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = this.Name;
            var dic = new Dictionary<string, object>();
            dic.Add("name", this.Name);
            dic.Add("displayName", this.DisplayName);

            dic.Add("links", this.Links);
            field.Value = dic;

            return field;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(DisplayName)
                && Links.Count > 0;
        }

        public void SetData(List<Attribute> attributes)
        {
            var links = attributes.FindAll(a => a is LinkAttribute).ToList();
            foreach(var attr in links)
            {
                var linkAttr = attr as LinkAttribute;
                Links.Add(new LinkAction()
                {
                    Name=linkAttr.FieldName,
                    DisplayName=linkAttr.FieldName,
                    Url=linkAttr.Url
                });
            }
            //throw new NotImplementedException();
        }
    }
}
