using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Tabs
{
    public class KBTab:IComponent
    {
        public ComponentType Type => ComponentType.KTab;

        public List<KTabItem> Tabs { get; set; } = new List<KTabItem>();

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = "tabs";

            var list = new List<Dictionary<string, object>>();
            foreach(var item in Tabs)
            {
                list.Add(item.GetData());
            }
            field.Value = list;

            return field;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

    public class KTabItem
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public IComponent Component { get; set; }

        public Dictionary<string,object> GetData()
        {
            var dic = new Dictionary<string, object>();

            dic.Add("name",Name);
            dic.Add("displayName", DisplayName);
            dic.Add("type", Component.Type);

            var field = Component.GetField();
            dic.Add("item", field);

            return dic;
        }
    }

}
