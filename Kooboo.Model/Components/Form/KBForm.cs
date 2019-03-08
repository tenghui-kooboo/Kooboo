using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Form
{
    public class KBForm:IComponent
    {
        public ComponentType Type => ComponentType.KForm;

        public List<FormField> Fields { get; set; } = new List<FormField>();

        //"fields:[]"
        public VueField GetField()
        {
            var vueField = new VueField();
            vueField.Name = "fields";

            var list = new List<Dictionary<string,object>>();
            foreach(var field in Fields)
            {
                list.Add(field.GetData());
            }
            vueField.Value = list;
            return vueField;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

    public class FormField
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public IBaseComponent Component { get; set; }

        public Dictionary<string,object> GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("name", Name);
            dic.Add("displayName", DisplayName);
            dic.Add("type", Component.Type.ToString());
            //some component needs data. 
            //like select component needs options
            dic.Add("data", Component.GetData());

            return dic;
        }
    }
}
